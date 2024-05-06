import { createStore } from "vuex";
import createPersistedState from 'vuex-persistedstate'
import Cookies from 'js-cookie'

export default createStore( {
    state: {
        user: {},
        cart: [],
        isAuth:false
    },
    getters: {
        getCart(state) {
            return state.cart
        },
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

        setCart(state, cart) {
            state.cart = cart
        },
        addToCart(state, item) {
            state.cart.push(item)
        },
        clearCart(state) {
            state.cart = []; //?? idk about leaks here
        },
        
        setIsAuth(state, isAuth) {
            state.isAuth = isAuth
        },
    },
    plugins: [createPersistedState({
        storage: {
          getItem: key => Cookies.get(key),
          setItem: (key, value) => Cookies.set(key, value, { expires: 3, secure: true }),
          removeItem: key => Cookies.remove(key)
        }
      })]
})