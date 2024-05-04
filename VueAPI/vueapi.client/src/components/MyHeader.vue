<template>
    <header class="menu"> 
        <a class="main_link" href="/">Главная</a>
        <ul v-if="!this.$store.getters.getIsAuth" class="auth-container">
            <li class="login_box">
                <a class="login_link" href="/login">Войти</a>
            </li>
            <li>
                <a class="register_link" href="/register">Зарегистрироваться</a>
            </li>
        </ul>
        <div v-if="this.$store.getters.getIsAuth">Здравствуй, {{ this.$store.getters.getUser.name }}</div>
    </header>
</template>
<script setup>

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
    margin-right:auto;
}

</style>