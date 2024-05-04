import './assets/main.css'

import { createStore } from "vuex";
import { createApp } from 'vue'
import App from "@/App.vue";
import router from './Router'
import store from './store';

export default createStore ( {
    state: {
        user: {},
        isAuth: false
    },
    getters: {
        getUser(state) {
            return state.user;
        }
    },
    mutations: {
        updateUser(state, newUser) {
            state.user = newUser
        }
    },
    actions: {

    },
    modules: {

    }
});

createApp(App)
    .use(router)
    .use(store)
    .mount('#app')
