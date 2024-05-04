import { createStore } from "vuex";
import createPersistedState from 'vuex-persistedstate'
import Cookies from 'js-cookie'

export default createStore( {
    state: {
        user: {},
        isAuth:false
    },
    getters: {
        getUser(state) {
            return state.user;
        },
        getIsAuth(state) {
            return state.isAuth;
        }
    },
    mutations: {
        setUser(state, user) {
            state.user = user
        },
        setIsAuth(state, isAuth) {
            state.isAuth = isAuth
        }
    },
    plugins: [createPersistedState({
        storage: {
          getItem: key => Cookies.get(key),
          setItem: (key, value) => Cookies.set(key, value, { expires: 3, secure: true }),
          removeItem: key => Cookies.remove(key)
        }
      })]
})