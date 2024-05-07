<template>
    <MyHeader></MyHeader>
    <h1>Корзина</h1>
    <div class="success">Заказ успешно создан</div>
    <div class="errorMessage" v-if="error">Произошла ошибка!</div>
    <div class="foodItemList">
        <div class="foodItem" v-for="item in cart" :key="item.foodName">
            <span>{{ item.foodName }}</span> <br>
            <span>{{ item.price }} Р</span> <br>
            <button @click="addCount(item)">+1</button> <!-- steal icon from danar -->
            <span>{{ item.count }}</span>
            <button @click="subCount(item)">-1</button>
        </div>
    </div>
    <div class="checkoutContainer">
        <div v-if="!this.emptyCart" class="priceCounter">{{ priceSum }} Р</div>
        <MyButton class="createOrder" @click="createOrder"> Оформить заказ </MyButton>
    </div>
    <div v-if="this.emptyCart"> Корзина пуста!</div>
    <MyButton class="clearButton" @click="clearCart">Очистить корзину</MyButton>
</template>

<script>
import MyButton from '@/components/MyButton.vue';
import MyHeader from '@/components/MyHeader.vue';
import axios from 'axios';
import store from '@/store';
import { integer } from '@vuelidate/validators';

export default {
    components: {
        MyHeader,
        MyButton,
    },
    data() {
        return {
            cart:[],
            error:false,
            success:false,
            count:integer,
        }
    },
    created() {
        //DANGER: USER CAN EDIT LOCALSTORAGE, SECURITY ISSUE
        if(localStorage.getItem("cart") != null && localStorage.getItem("cart") != "")
            this.cart = JSON.parse(localStorage.getItem("cart"))
    },
    updated() {
        if(localStorage.getItem("cart") != null && localStorage.getItem("cart") != "")
            localStorage.setItem("cart", JSON.stringify(this.cart))
    },
    computed: {
        emptyCart() {
            return this.cart == "";
        },
        priceSum() {
            if(!this.emptyCart) {
                return this.cart
                    .flatMap(item => item.price*item.count)
                    .reduce((accumulator, currentValue) => accumulator + currentValue)
                }
            else return null;
        }
    },
    methods: {
        //store cart on unload
        addCount(foodItem) {
            foodItem.count += 1; //these functions need to change values in store, not in local model
        },
        subCount(foodItem) {
            if(foodItem.count > 1) 
                foodItem.count -= 1;
            //else ask permission to delete item from cart
        },
        clearCart() {
            localStorage.setItem("cart", "")
            this.cart = [];
        },
        confirmRequest() {
            this.success = true;
            this.clearCart();
        },
        createOrderDetails(orderBody) {
            this.cart.forEach(item => {
                    const orderDetailsRequestBody = {
                        FoodItem: item.foodID,
                        Quantity: item.count,
                        OrderID: orderBody.orderID
                    }
                    axios.post("http://localhost:5174/api/OrderDetail", orderDetailsRequestBody)
                });
        },
        createOrder() { //need to make sure if user exists
            const requestBody = {
                CustomerID: store.getters.getUser.customerID, 
                address: store.getters.getUser.lastAddress, 
                }
            try {
                axios.post("http://localhost:5174/api/Order", requestBody)
                    .then(response => response.data)
                    .then(data => this.createOrderDetails(data))
                    .then(() => this.confirmRequest())
                
                
            }
            catch (error) {
                this.error = true;
            }
            //this.$router.push("/createOrder")
        }
    },
}
</script>

<style scoped>
.foodItemList {
    display:flex;
    flex-direction: column;

}

.foodItem {
    border:1px solid teal;
}

</style>