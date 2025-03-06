import axios, { AxiosError } from "axios";
import { getUser } from "./Auth/AuthContext";
import { AuthUser } from "../../model/User/AuthUser";
import { RefreshRequest } from "../../model/User/Request/RefreshRequest";
import { UserService } from "./Auth/UserService";

const API_BASE_URL = "http://localhost:5205/api";

const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 5000,
});
const getAccessToken = () => {
  const user = UserService.getUser();

  if (!user) {
    return null;
  }
  return user.access_token;
};

const getRefreshToken = () => {
  const user = UserService.getUser();

  if (!user) {
    return null;
  }
  return user.refresh_token;
};

const refreshAccessToken = async (error: AxiosError) => {
  try {
    const refreshToken = getRefreshToken();
    console.log(refreshToken);
    if (!refreshToken) {
      throw new Error("Token non existant");
    }
    const response = await api.post<AuthUser>("/auth/refresh", {
      refresh_token: refreshToken,
    } as RefreshRequest);
    UserService.setUser(response.data);
  } catch (e) {
    console.log(e);
    UserService.clearUser();
    window.dispatchEvent(new Event("forcedLogout"));
    throw e;
  }
};

api.interceptors.request.use((request) => {
  const token = getAccessToken();
  if (token) {
    request.headers["Authorization"] = `Bearer ${token}`;
  }
  return request;
});

api.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    if (error.response && error.response.status === 401) {
      refreshAccessToken(error)
        .then(() => {
          const originalRequest = error.config;
          originalRequest.headers[
            "Authorization"
          ] = `Bearer ${getAccessToken()}`;
          return api.request(originalRequest);
        })
        .catch(() => {});
    } else if (error.code === "ERR_NETWORK") {
      window.dispatchEvent(new Event("hailFailed"));
    }
    throw new Error("Refresh failed");
  }
);

export default api;
