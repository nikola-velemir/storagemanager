import React, { FormEvent, useContext, useEffect, useState } from "react";
import styles from "./LoginForm.module.css";
import login_image from "../../../assets/login-image.jpg";
import { AuthUser } from "../../../model/User/AuthUser";
import { AuthService } from "../../../services/AuthService";
import { validatePassword, validateUsername } from "./LoginFormValidators";
import { useNavigate } from "react-router-dom";
import { LoginRequest } from "../../../model/User/Request/LoginRequest";
import { useAuth } from "../../../infrastructure/AuthContext";
import ResponseModal from "../../common/SuccessModal/ResponseModal";
import SuccessButton from "../../common/SuccessButton/SuccessButton";
import api from "../../../infrastructure/Interceptor";

interface LoginErrors {
  username: string;
  password: string;
}

const LoginForm = () => {
  const userNameErrorText = "Username cannot be blank";
  const passwordErrorText = "Password must be atleast 4 characters long";
  const userContext = useAuth();

  useEffect(() => {
    const token = userContext.user?.access_token;
    if (token) {
      api.defaults.headers["Authorization"] = `Bearer ${token}`;
    }
  }, [userContext]);
  const [credentials, setCredentials] = useState<LoginRequest>({
    username: "",
    password: "",
  });
  const [errors, setErrors] = useState<LoginErrors>({
    username: userNameErrorText,
    password: passwordErrorText,
  });
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);

  const [modalSuccess, setModalSuccess] = useState(false);

  const handleNavigate = () => {
    navigate("/");
  };

  const handleModal = (sucess: boolean) => {
    handleOpenModal(sucess);
    setTimeout(() => {
      handleCloseModal();
      if (sucess) {
        handleNavigate();
      }
    }, 3000);
  };
  const handleOpenModal = (success: boolean) => {
    setModalSuccess(success);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setCredentials({ ...credentials, [name]: value });

    if (name === "username") {
      setErrors({
        ...errors,
        username: validateUsername(value) ? "" : userNameErrorText,
      });
    }
    if (name === "password") {
      setErrors({
        ...errors,
        password: validatePassword(value) ? "" : passwordErrorText,
      });
    }
  };
  const handleLogin = (e: FormEvent) => {
    e.preventDefault();
    if (errors.username || errors.password) {
      return;
    }
    console.log(credentials);
    AuthService.login(credentials)
      .then((next) => {
        userContext.setUser(next.data);
        handleModal(true);
      })
      .catch((error) => {
        console.log(error);
        handleModal(false);
      });
  };
  return (
    <>
      {showModal && (
        <ResponseModal>
          {modalSuccess ? (
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke-width="1.5"
              className="size-32 stroke-green-500"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                d="M9 12.75 11.25 15 15 9.75M21 12c0 1.268-.63 2.39-1.593 3.068a3.745 3.745 0 0 1-1.043 3.296 3.745 3.745 0 0 1-3.296 1.043A3.745 3.745 0 0 1 12 21c-1.268 0-2.39-.63-3.068-1.593a3.746 3.746 0 0 1-3.296-1.043 3.745 3.745 0 0 1-1.043-3.296A3.745 3.745 0 0 1 3 12c0-1.268.63-2.39 1.593-3.068a3.745 3.745 0 0 1 1.043-3.296 3.746 3.746 0 0 1 3.296-1.043A3.746 3.746 0 0 1 12 3c1.268 0 2.39.63 3.068 1.593a3.746 3.746 0 0 1 3.296 1.043 3.746 3.746 0 0 1 1.043 3.296A3.745 3.745 0 0 1 21 12Z"
              />
            </svg>
          ) : (
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              className="size-32 stroke-red-500"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="m9.75 9.75 4.5 4.5m0-4.5-4.5 4.5M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"
              />
            </svg>
          )}
        </ResponseModal>
      )}
      <div className={`${styles.login}`}>
        <div
          className={`${styles.formBackGround} `}
          style={{ background: `url(${login_image})` }}
        ></div>
        <div className={`${styles.formContainer} backdrop-blur-sm`}>
          <form
            className="bg-slate-800 mx-auto rounded-3xl h-fit py-40 px-20"
            style={{ width: "50rem" }}
          >
            <div className="mb-5">
              <label
                htmlFor="email"
                className="block mb-2 text-md font-medium text-white dark:text-white"
              >
                Your username
              </label>
              <input
                type="text"
                id="username"
                name="username"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                required
                value={credentials.username}
                onChange={handleChange}
                onBlur={handleChange}
              />
              <div className="text-red-600">
                {errors.username.length > 0 ? errors.username : ""}
              </div>
            </div>
            <div className="mb-5">
              <label
                htmlFor="password"
                className="block mb-2 text-md font-medium text-white dark:text-white"
              >
                Your password
              </label>
              <input
                type="password"
                id="password"
                name="password"
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                required
                value={credentials.password}
                onChange={handleChange}
                onBlur={handleChange}
              />
              <div className="text-red-600">
                {errors.password.length > 0 ? errors.password : ""}
              </div>
            </div>
            <div className="flex w-full justify-center">
              <SuccessButton
                onClick={handleLogin}
                text="Log in"
              ></SuccessButton>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default LoginForm;
