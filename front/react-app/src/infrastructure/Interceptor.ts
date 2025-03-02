import axios from "axios";
import { getUser } from "./AuthContext";

const userContext = getUser();

const API_BASE_URL = "http://localhost:5205";

const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 5000,
});
const getToken = () => {
  console.log(userContext.access_token);
  return userContext.access_token;
};

api.interceptors.request.use((config) => {
  const token = getToken();
  if (token) {
    config.headers["Authorization"] = `Bearer ${token}`;
  }
  return config;
});

export default api;
