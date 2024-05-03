import { email, helpers, minLength, required } from '@vuelidate/validators';

export const required$ = helpers.withMessage("Это поле не должно оставаться пустым", required)

export const minLength$ = (min) => helpers.withMessage(
    "Поле должно содержать не менее $min символов.", minLength(min));

export const email$ = helpers.withMessage("Некорректный адрес электронной почты", email)

export const validPhoneNumber = helpers.regex(/^[+]?[0-9]{11}$/);
export const validPhoneNumber$ = helpers.withMessage("Некорректный номер телефона", validPhoneNumber)