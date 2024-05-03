import { createRouter, createWebHistory } from 'vue-router';
import RegisterPage from "./pages/RegisterPage.vue";
import LoginPage from "./pages/LoginPage.vue";
import CardHolder from './components/CardHolder.vue';

const routes = [
    {path: '/', component: CardHolder},
    {path: '/register', component: RegisterPage},
    {path: '/login', component: LoginPage},
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router