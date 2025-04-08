export const validateTextField = (text: string) => {
  return text.trim().length > 0;
};
export const validatePhoneField = (text: string) => {
  const phoneRegex = /^\+?[0-9]{10,15}$/; // accepts optional '+' and 10 to 15 digits
  const parsedText = text.trim();
  return parsedText.length > 0 && phoneRegex.test(parsedText);
};
