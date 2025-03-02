import React, { FormEvent, useContext, useState } from "react";
import styles from "./LoginForm.module.css";
import login_image from "../../../assets/login-image.jpg";
import { AuthUser } from "../../../model/User/AuthUser";
import { LoginService } from "../../../services/LoginService";
import { validatePassword, validateUsername } from "./LoginFormValidators";
import { useNavigate } from "react-router-dom";
import { LoginRequest } from "../../../model/User/Request/LoginRequest";
import { useAuth } from "../../../infrastructure/AuthContext";
import ResponseModal from "../../common/SuccessModal/ResponseModal";

interface LoginErrors {
  username: string;
  password: string;
}

const LoginForm = () => {
  const userContext = useAuth();
  const [credentials, setCredentials] = useState<LoginRequest>({
    username: "",
    password: "",
  });
  const [errors, setErrors] = useState<LoginErrors>({
    username: "Username cannot be blank",
    password: "Password must be atleast 8 characters long",
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
        username: validateUsername(value) ? "" : "Username cannot be blank",
      });
    }
    if (name === "password") {
      setErrors({
        ...errors,
        password: validatePassword(value)
          ? ""
          : "Password must be atleast 8 characters long",
      });
    }
  };
  const handleLogin = (e: FormEvent) => {
    e.preventDefault();
    if (errors.username || errors.password) {
      return;
    }
    LoginService.login(credentials)
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
              fill="#28a745"
              className="bi bi-check2-circle"
              viewBox="0 0 16 16"
            >
              <path d="M2.5 8a5.5 5.5 0 0 1 8.25-4.764.5.5 0 0 0 .5-.866A6.5 6.5 0 1 0 14.5 8a.5.5 0 0 0-1 0 5.5 5.5 0 1 1-11 0" />
              <path d="M15.354 3.354a.5.5 0 0 0-.708-.708L8 9.293 5.354 6.646a.5.5 0 1 0-.708.708l3 3a.5.5 0 0 0 .708 0z" />
            </svg>
          ) : (
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="16"
              height="16"
              fill="#ff6f20"
              className="bi bi-x-circle"
              viewBox="0 0 16 16"
            >
              <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
              <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708" />
            </svg>
          )}
        </ResponseModal>
      )}
      <div className={`${styles.login}`}>
        <div
          className={`${styles.formBackGround}`}
          style={{ background: `url(${login_image})` }}
        ></div>

        <div className={`${styles.formContainer}`}>
          <form className={`${styles.form}`}>
            <div className="mb-3">
              <label className="form-label mx-2">Username</label>
              <input
                type="text"
                className="form-control"
                id="exampleInputEmail1"
                name="username"
                value={credentials.username}
                required
                onChange={handleChange}
                onBlur={handleChange}
                aria-describedby="emailHelp"
              />
              <p style={{ color: "red" }}>
                {errors.username.length > 0 ? errors.username : ""}
              </p>
            </div>
            <div className="mb-3">
              <label className="form-label mx-2">Password</label>
              <input
                type="password"
                className="form-control"
                name="password"
                required
                onChange={handleChange}
                onBlur={handleChange}
                value={credentials.password}
                id="exampleInputPassword1"
              />
              <p style={{ color: "red" }}>
                {errors.password.length > 0 ? errors.password : ""}
              </p>
            </div>
            <div className={`${styles.loginButtonContainer} mt-5`}>
              <button
                type="button"
                className="btn btn-success"
                onClick={handleLogin}
              >
                Login
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default LoginForm;
