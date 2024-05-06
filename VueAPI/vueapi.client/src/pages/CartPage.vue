<template>
    <MyHeader></MyHeader>
    <h1>Корзина</h1>
    <div class="foodItemList">
        <div class="foodItem" v-for="item in cart" :key="item.foodName">
            <span>{{ item }}</span> <br>
            <!-- <span>{{ item }}</span> <br>
            <span>{{ item }}</span> <br> -->
        </div>
    </div>
    <MyButton class="clearButton" @click="clearCart">Очистить корзину</MyButton>
</template>

<script>
import MyButton from '@/components/MyButton.vue';
import MyHeader from '@/components/MyHeader.vue';
import store from '@/store';

export default {
    components: {
        MyHeader,
        MyButton,
    },
    data() {
        return {
            cart:[],
        }
    },
    created() {
        this.cart = store.getters.getCart
    },
    methods: {
        clearCart() {
            store.commit("clearCart")
            this.cart = [];
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