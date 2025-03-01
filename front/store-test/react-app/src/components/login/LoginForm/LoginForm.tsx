import React, { FormEvent, use, useState } from "react";
import styles from "./LoginForm.module.css";
import login_image from "../../../assets/login-image.jpg";
import { url } from "inspector";
import { log } from "console";
import { AuthUser } from "../../../model/user/AuthUser";
import { LoginService } from "../../../services/LoginService";
import { validatePassword, validateUsername } from "./LoginFormValidators";
import SucessModal from "../../common/SuccessModal/SucessModal";
import { useNavigate } from "react-router-dom";

interface LoginErrors {
  username: string;
  password: string;
}

const LoginForm = () => {
  const [credentials, setCredentials] = useState<AuthUser>({
    username: "",
    password: "",
    role: "admin",
  });
  const [errors, setErrors] = useState<LoginErrors>({
    username: "Username cannot be blank",
    password: "Password must be atleast 8 characters long",
  });
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);

  const handleNavigate = () => {
    navigate("/");
  };

  const handleModal = () => {
    handleOpenModal();
    setTimeout(() => {
      handleCloseModal();
      handleNavigate();
    }, 3000);
  };
  const handleOpenModal = () => {
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
    LoginService.login(credentials);
    handleModal();
  };
  return (
    <>
      {showModal && <SucessModal />}
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
