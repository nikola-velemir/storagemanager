import React, { FormEvent, useContext, useEffect, useState } from "react";
import styles from "./LoginForm.module.css";
import login_image from "../../../assets/login-image.jpg";
import { AuthUser } from "../../../model/userModels/AuthUser";
import { AuthService } from "../../../services/AuthService";
import { validatePassword, validateUsername } from "./LoginFormValidators";
import { useNavigate } from "react-router-dom";
import { LoginRequest } from "../../../model/userModels/Request/LoginRequest";
import { useAuth } from "../../../infrastructure/Auth/AuthContext";
import ResponseModal from "../../common/modals/ResponseModal/ResponseModal";
import SuccessButton from "../../common/buttons/SuccessButton/SuccessButton";
import api from "../../../infrastructure/Interceptor/Interceptor";
import { Modal } from "flowbite-react";

interface LoginErrors {
  username: string;
  password: string;
}

enum ModalState {
  NOT_DISPLAYING,
  LOADING,
  SUCCESS,
  ERROR,
}

const LoginForm = () => {
  const userNameErrorText = "Username cannot be blank";
  const passwordErrorText = "Password must be atleast 4 characters long";
  const userContext = useAuth();

  useEffect(() => {
    const token = userContext.user?.accessToken;
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

  const [modalState, setModalState] = useState<ModalState>(
    ModalState.NOT_DISPLAYING
  );

  const handleNavigate = () => {
    navigate("/");
  };

  const handleModalResponse = (state: ModalState) => {
    handleCloseModal();
    handleOpenModal(state);
    setTimeout(() => {
      handleCloseModal();
      if (state) {
        handleNavigate();
      }
    }, 2000);
  };
  const handleOpenModal = (state: ModalState) => {
    setModalState(state);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setModalState(ModalState.NOT_DISPLAYING);
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
    handleOpenModal(ModalState.LOADING);
    AuthService.login(credentials)
      .then((next) => {
        setTimeout(() => {
          userContext.setUser(next.data);
          handleModalResponse(ModalState.SUCCESS);
        }, 1000);
      })
      .catch(() => {
        setTimeout(() => {
          handleModalResponse(ModalState.ERROR);
        }, 1000);
      });
  };
  return (
    <>
      {showModal && (
        <ResponseModal>
          {modalState === ModalState.SUCCESS ? (
            <div className="flex justify-center items-center flex-col">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth="1.5"
                className="size-36 stroke-green-500"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="M9 12.75 11.25 15 15 9.75M21 12c0 1.268-.63 2.39-1.593 3.068a3.745 3.745 0 0 1-1.043 3.296 3.745 3.745 0 0 1-3.296 1.043A3.745 3.745 0 0 1 12 21c-1.268 0-2.39-.63-3.068-1.593a3.746 3.746 0 0 1-3.296-1.043 3.745 3.745 0 0 1-1.043-3.296A3.745 3.745 0 0 1 3 12c0-1.268.63-2.39 1.593-3.068a3.745 3.745 0 0 1 1.043-3.296 3.746 3.746 0 0 1 3.296-1.043A3.746 3.746 0 0 1 12 3c1.268 0 2.39.63 3.068 1.593a3.746 3.746 0 0 1 3.296 1.043 3.746 3.746 0 0 1 1.043 3.296A3.745 3.745 0 0 1 21 12Z"
                />
              </svg>
              <div className="text-green-500 bold font-bold text-lg">
                Welcome!
              </div>
            </div>
          ) : modalState === ModalState.ERROR ? (
            <div className="flex justify-center items-center flex-col">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth={1.5}
                className="size-36 stroke-red-700"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="m9.75 9.75 4.5 4.5m0-4.5-4.5 4.5M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"
                />
              </svg>
              <div className="text-red-700 bold font-bold text-lg">
                Provided credentials are incorrect!
              </div>
            </div>
          ) : (
            <div role="status">
              <svg
                aria-hidden="true"
                className="inline w-28 h-28 text-gray-200 animate-spin dark:text-gray-600 fill-gray-600 dark:fill-gray-300"
                viewBox="0 0 100 101"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                  fill="currentColor"
                />
                <path
                  d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                  fill="currentFill"
                />
              </svg>
              <span className="sr-only">Loading...</span>
            </div>
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
              <div className="h-8 text-red-600">
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
              <div className="h-8 text-red-600">
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
