<script>
import MyHeader from '@/components/MyHeader.vue';
import { email$, minLength$, required$, validPhoneNumber$ } from '@/validators';
import useVuelidate from '@vuelidate/core';


import axios from 'axios';



export default {
    setup() {
        return { v$: useVuelidate() }
    },
    components : {
        MyHeader,
    },
    data() {
        return {
            name: "",
            address: "",
            phoneNumber: "",
            password: "",
            email: "",
            postedId: "",
            posted: false
        }
    },
    validations() {
        return {
            name: { required$ , minLength : minLength$(3)},
            phoneNumber: { required$, validPhoneNumber$ },
            address: { required$, minLength : minLength$(10)},
            email: { required$, email$, minLength : minLength$(3)},
            password: { required$, minLength : minLength$(5)},
        }
    },
    
    methods: {
        postUser() {
            this.v$.$validate();
            if(!this.v$.$error)
            {
                const user = { name: this.name, lastAddress: this.address, phoneNumber: this.phoneNumber, email: this.email, passHash : this.password }
                try {
                    axios.post("http://localhost:5174/api/Customer", user)
                    .catch(() => this.error = true)
                    .then(() => this.posted = true)
                    .then(alert("Регистрация прошла успешно!"))
                    .then(this.$router.push('/'))
                }
                catch(error) {
                    alert("Что-то пошло не так") 
                }
            }

        }
    }
}
</script>

<template>
    <MyHeader />

    <div class="registerForm">
        <form class="form">
            <h2>Регистрация пользователя</h2>
            <input v-model.trim="name" class="input" type="text" placeholder="Имя">
            <span class="errorSpan" v-if="v$.name.$error">{{v$.name.$errors[0].$message}}</span>
            <input v-model.trim="address" class="input" type="text" placeholder="Адрес">
            <span class="errorSpan" v-if="v$.address.$error">{{v$.address.$errors[0].$message}}</span>
            <input v-model.trim="email" class="input" type="text" placeholder="Электронная почта">
            <span class="errorSpan" v-if="v$.email.$error">{{v$.email.$errors[0].$message}}</span>
            <input v-model.trim="phoneNumber" class="input" type="text" placeholder="Номер телефона">
            <span class="errorSpan" v-if="v$.phoneNumber.$error">{{v$.phoneNumber.$errors[0].$message}}</span>
            <input v-model.trim="password" class="input" type="text" placeholder="Пароль">
            <span class="errorSpan" v-if="v$.password.$error">{{v$.password.$errors[0].$message}}</span>
            <button class="registerButton" type="button" @click="postUser">Зарегистрироваться</button>
        </form>
        <div v-if="posted">
            Регистрация прошла успешно!
        </div>
    </div>
</template>



<style scoped>
.form {
    display: flex;
    flex-direction: column;
    width:60%;
}

.registerForm {
    padding: 20px;
    min-width: 720px;
}

.input {
    width: 100%;
    border: 1px solid teal;
    margin-top: 15px;
    padding: 10px 15px
}

.errorSpan {
    color: darkred  ;
}
.registerButton {
    border: 1px solid teal;
    padding: 5px;
    background-color: white;
    margin-top: 15px;
    align-self: flex-end;
}
</style>