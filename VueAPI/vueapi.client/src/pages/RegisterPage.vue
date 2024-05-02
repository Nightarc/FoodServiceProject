<template>
    <div class="registerForm">
        <form class="form">
            <h2>Регистрация пользователя</h2>
            <input :value="name" 
                @input="name = $event.target.value" 
                class="input" 
                type="text"            
                placeholder="Имя">
            <input 
                :value="address" 
                @input="address = $event.target.value" 
                class="input" type="text"         
                placeholder="Адрес">
            <input 
                :value="email" 
                @input="email = $event.target.value"
                class="input" 
                type="text"           
                placeholder="Электронная почта">
            <input 
                :value="phoneNumber" 
                @input="phoneNumber = $event.target.value" 
                class="input" type="text"     
                placeholder="Номер телефона">
            <button class="registerButton" @click="postUser">Зарегистрироваться</button>
        </form>
    </div>
</template>

<script>
export default {
    data() {
        return {
            name: "",
            address: "",
            phoneNumber: "",
            email: ""
        }
    },
    methods: {
        postUser() {
            
        }
        }
    }
}
</script>

<style scoped>
* {
    padding: 0;
    box-sizing: border-box;
}

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

.registerButton {
    border: 1px solid teal;
    padding: 5px;
    background-color: white;
    margin-top: 15px;
    align-self: flex-end;
}
</style>