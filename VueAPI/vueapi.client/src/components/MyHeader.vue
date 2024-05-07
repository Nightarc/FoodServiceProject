<template>
    <header class="menu"> 
        <a class="main_link" href="/">Главная</a>
        <a class="cart_link" href="/cart">Корзина</a>
        <ul v-if="!$store.getters.getIsAuth" class="auth-container">
            <li class="login_box">
                <a class="login_link" href="/login">Войти</a>
            </li>
            <li>
                <a class="register_link" href="/register">Зарегистрироваться</a>
            </li>
        </ul>
        <div v-if="$store.getters.getIsAuth">Здравствуй, {{ $store.getters.getUser.name }}</div>
        <div v-if="$store.getters.getIsAuth">
            <MyButton @click="logout">Выйти</MyButton>
        </div>
    </header>
</template>
<script setup>
import store from '@/store';
import MyButton from './MyButton.vue';
const logout = () => {
    store.commit("setUser", null);
    store.commit("setIsAuth", false);
    localStorage.removeItem("cart")
    this.$router.push("/")
}

</script>
<style>
.auth-container {
    display:flex;
    float: right;
    list-style: none;
    gap:15px;
}
.menu {
    display:flex;
    flex-direction: row;
    background-color: gainsboro;
    padding:0.5rem;
    justify-content: flex-end;
}
.register_link {
    border: 1px solid teal;
    background-color: teal;
    color:white;
    align-self: flex-end;
    order: 2;
}

.login_link {
    border: 1px solid teal;
    background-color: teal;
    color:white;
    align-self: flex-end;
}

.main_link {
    border: 1px solid teal;
    background-color: teal;
    color:white;
}

.cart_link {
    border: 1px solid teal;
    background-color: teal;
    color:white;
    margin-left: 5px;
    margin-right:auto;
}
</style>