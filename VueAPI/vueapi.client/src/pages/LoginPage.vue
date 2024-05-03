<template>
    <MyHeader />
    <div class="registerForm">
        <form class="form">
            <h2>Вход в учетную запись</h2>
            <input v-model.trim="email" class="input" type="text" placeholder="Электронная почта">
            <input v-model.trim="password" class="input" type="text" placeholder="Пароль">
            <button class="loginButton" type="button" @click="submitForm">Войти</button>
        </form>
        <div v-if="loggedin">Вход произошел успешно!</div>
    </div>
</template>

<script>
import MyHeader from '@/components/MyHeader.vue';
import axios from 'axios';

export default {
    components: {
        MyHeader,
    },
    data() {
        return {
            email: "",
            password: "",
            address: "",
            error: false,
            postedId: "",
            loggedin: false
        }
    },
    methods: {
        submitForm() {
            const requestBody = {name:"dummy", email: this.email, phoneNumber: "+00000000000" , passHash : this.password } //! this will be required to change if structure of auth changes
            try {
                axios.post("http://localhost:5174/api/Customer/pass", requestBody)
                    .then(response => response.data)
                    .then(data => {this.address = data.address})
            }
            catch (error) {
                this.error = true;
            }
            
            if(this.address != "") {
                this.loggedin = true;
            }

        }
    }
}
</script>

<style scoped>
.form {
    display: flex;
    flex-direction: column;
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

.loginButton {
    border: 1px solid teal;
    padding: 5px;
    background-color: white;
    margin-top: 15px;
    align-self: flex-end;
}
</style>