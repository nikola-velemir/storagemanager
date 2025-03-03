import axios, { AxiosError } from "axios";
import { getUser } from "./AuthContext";
import { AuthUser } from "../model/User/AuthUser";
import { RefreshRequest } from "../model/User/Request/RefreshRequest";

let userContext = getUser();

const API_BASE_URL = "http://localhost:5205/api";

const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 5000,
});
const getAccessToken = () => {
  if (!userContext) {
    return null;
  }
  console.log(userContext.access_token);
  return userContext.access_token;
};

const getRefreshToken = () => {
  if (!userContext) {
    return null;
  }
  return userContext.refresh_token;
};

const refreshAccessToken = async (error: AxiosError) => {
  try {
    if (!getRefreshToken()) {
      throw error;
    }
    const response = await api.post<AuthUser>("/refresh", {
      refresh_token: getRefreshToken(),
    } as RefreshRequest);
    localStorage.setItem("user", JSON.stringify(response.data));
  } catch (e) {
    localStorage.removeItem("user");
  } finally {
    userContext = getUser();
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
  (error) => {
    if (error.response && error.response.status === 401) {
      refreshAccessToken(error);
    }
    throw error;
  }
);

export default api;
