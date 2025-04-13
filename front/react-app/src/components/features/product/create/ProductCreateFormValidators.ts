export const validateField = (field: string) => {
  return field.trim().length !== 0;
};
export const validateIndentifier = (identifier: string) => {
  return identifier.trim().length >= 6;
};
