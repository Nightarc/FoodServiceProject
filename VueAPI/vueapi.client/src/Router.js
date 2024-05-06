import { createRouter, createWebHistory } from 'vue-router';
import RegisterPage from "./pages/RegisterPage.vue";
import LoginPage from "./pages/LoginPage.vue";
import MainPage from './pages/MainPage.vue';
import CartPage from './pages/CartPage.vue';

const routes = [
    {path: '/', component: MainPage},
    {path: '/register', component: RegisterPage},
    {path: '/login', component: LoginPage},
    {path: '/cart', component: CartPage},
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router