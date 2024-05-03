<template>
    <MyHeader />
    <div class="registerForm">
        <form class="form">
            <h2>Вход в учетную запись</h2>
            <input 
                v-model.trim="phoneNumber"
                class="input" type="text"     
                placeholder="Номер телефона">
            <button class="loginButton" type="button" @click="postUser">Войти</button>
        </form>
    </div>
</template>

<script>
import MyHeader from '@/components/MyHeader.vue';
import axios from 'axios';

export default {
    components : {
        MyHeader,
    },
    data() {
        return {
            name: "",
            address: "",
            phoneNumber: "",
            email: "",
            error:false,
            postedId:"",
            posted:false    
        }
    },
    methods: {
        postUser() {
            const user = {name:this.name, lastAddress:this.address, phoneNumber:this.phoneNumber, email:this.email}
            try{
                axios.post("http://localhost:5174/api/Customer", user)
                    .catch(() => this.error = true)
                    .then(() => this.posted = true)
            }
            catch(error) {
                this.error = true;
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