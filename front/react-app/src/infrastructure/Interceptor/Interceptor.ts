import axios from "axios";
import { AuthUser } from "../../model/userModels/AuthUser";
import { RefreshRequest } from "../../model/userModels/Request/RefreshRequest";
import { UserService } from "../Auth/UserService";

const API_BASE_URL = "http://192.168.1.8:5205/api";

const api = axios.create({
  baseURL: API_BASE_URL,
  //timeout: 5000,
});
const getAccessToken = () => {
  const user = UserService.getUser();

  if (!user) {
    return null;
  }
  return user.accessToken;
};

const getRefreshToken = () => {
  const user = UserService.getUser();

  if (!user) {
    return null;
  }
  return user.refreshToken;
};

const refreshAccessToken = async () => {
  const refreshToken = getRefreshToken();
  if (!refreshToken) {
    throw new Error("Token non existant");
  }
  const response = await api.post<AuthUser>("/auth/refresh", {
    refreshToken: refreshToken,
  } as RefreshRequest);
  return response.data;
};

const forceLogoutUser = () => {
  UserService.clearUser();
  window.dispatchEvent(new Event("forcedLogout"));
};

api.interceptors.request.use((request) => {
  const token = getAccessToken();
  if (token) {
    request.headers["Authorization"] = `Bearer ${token}`;
  }
  return request;
});

const resendRequest = (error: any) => {
  const originalRequest = error.config;
  originalRequest.headers["Authorization"] = `Bearer ${getAccessToken()}`;
  return api.request(originalRequest);
};

api.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    if (error.response && error.response.status === 401) {
      try {
        const newUser = await refreshAccessToken();
        UserService.setUser(newUser);
        return resendRequest(error);
      } catch (e) {
        forceLogoutUser();
        return Promise.reject(e);
      }
    } else if (error.code === "ERR_NETWORK") {
      window.dispatchEvent(new Event("hailFailed"));
    }
    return Promise.reject(error);
  }
);

export default api;
