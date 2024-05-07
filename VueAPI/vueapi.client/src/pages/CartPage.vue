<template>
    <MyHeader></MyHeader>
    <h1>Корзина</h1>
    <div class="success">Заказ успешно создан</div>
    <div class="errorMessage" v-if="error">Произошла ошибка!</div>
    <div class="foodItemList">
        <div class="foodItem" v-for="item in cart" :key="item.foodName">
            <span>{{ item.foodName }}</span> <br>
            <span>{{ item.price }} Р</span> <br>
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
        }
    },
    created() {
        //DANGER: USER CAN EDIT LOCALSTORAGE, SECURITY ISSUE
        if(localStorage.getItem("cart") != null && localStorage.getItem("cart") != "")
        this.cart = JSON.parse(localStorage.getItem("cart"))
        console.log(this.cart)

    },
    computed: {
        emptyCart() {
            return this.cart == "";
        },
        priceSum() {
            if(!this.emptyCart) {
                return this.cart
                    .flatMap(item => item.price)
                    .reduce((accumulator, currentValue) => accumulator + currentValue)
                }
            else return null;
        }
    },
    methods: {
        clearCart() {
            localStorage.setItem("cart", "")
            this.cart = [];
        },
        confirmRequest() {
            this.success = true;
            this.clearCart();
        },
        createOrderDetails(orderBody) {
            console.log(orderBody)
            this.cart.forEach(item => {
                    const orderDetailsRequestBody = {
                        FoodItem: item.foodID,
                        Quantity: 1,
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
                console.log(requestBody)
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