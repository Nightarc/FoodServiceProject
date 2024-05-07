<template>
    <MyHeader></MyHeader>
    <h1>Корзина</h1>
    <div class="success">Заказ успешно создан</div>
    <div class="errorMessage" v-if="error">Произошла ошибка!</div>
    <div class="foodItemList">
        <div class="foodItem" v-for="item in cart" :key="item.foodName">
            <span>{{ item.foodName }}</span> <br>
            <span>{{ item.price }} Р</span> <br>
            <button @click="subCount(item)">-1</button>
            <span>{{ item.count }}</span>
            <button @click="addCount(item)">+1</button> <!-- steal icon from danar -->
        </div>
    </div>
    <div class="checkoutContainer">
        <div v-if="!this.emptyCart" class="priceCounter">{{ priceSum }} Р</div>
        <MyButton class="createOrder" @click="createOrder"> Оформить и оплатить заказ </MyButton>
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
        createPaymentUpdateOrder(orderBody) {
            
            const postRequestBody = {
                        NetPrice: this.priceSum,  
                        PaymentAmount: this.priceSum,
                        OrderID: orderBody.orderID
                    }
                    axios.post("http://localhost:5174/api/Payment", postRequestBody)
                        .then(response => response.data)
                        .then(data => {
                            const orderUpdateRequest = {
                                OrderID: orderBody.OrderID,
                                Address: orderBody.Address,
                                PaymentID: data.paymentID,
                                Time: orderBody.Time,
                                Date: orderBody.Date,
                                CustomerID: orderBody.CustomerID,
                                DeliveryPointID: orderBody.DeliveryPointID
                            }
                            axios.put("http://localhost:5174/api/Order", orderUpdateRequest)
                                .then(() => console.log("Оплата прошла успешно"))
                        })
            
        },
        createPayment(orderBody) {
            console.log("PAYMENT")
            console.log(orderBody)
            const postRequestBody = {
                        NetPrice: this.priceSum,  
                        PaymentAmount: this.priceSum,
                        OrderID: orderBody.orderID
                    }
                    axios.post("http://localhost:5174/api/Payment", postRequestBody)
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
        fillOrder(orderBody) {
            this.createOrderDetails(orderBody)
            this.createPayment(orderBody)
        },
        createOrder() { 
            if(store.getters.getUser != null) {//need to make sure if user exists
                const requestBody = {
                    CustomerID: store.getters.getUser.customerID, 
                    address: store.getters.getUser.lastAddress, 
                    }
                try {
                    axios.post("http://localhost:5174/api/Order", requestBody)
                        .then(response => response.data)
                        .then(data => this.fillOrder(data))
                        .then(() => this.confirmRequest())
                    
                    
                }
                catch (error) {
                    this.error = true;
                }
            }
            else this.$router.push("/login")
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