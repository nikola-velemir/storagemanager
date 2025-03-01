export const validateUsername = (username: string) => {
  return username.length > 0;
};

export const validatePassword = (password: string) => {
  return password.length >= 8;
};
