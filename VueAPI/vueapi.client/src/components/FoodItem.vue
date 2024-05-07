<template>
    <FoodPopup
        :is-open="isPopupOpen"
        :imagePath="foodInfo.imagePath"
        @ok="popupConfirmed"
        @close="isPopupOpen = false"
        >
            <template #title>{{ foodInfo.foodName }}</template>
            <template #description>{{foodInfo.description}}</template>
            <template #components>{{foodInfo.components}}</template>
            <template #price>{{foodInfo.price}}</template>
            <template #image> {{foodInfo.imagePath}} </template>

    </FoodPopup>
    <div class="FoodItemCard" @click="openPopup">
        <div class="FoodItem">
            <div class="FoodImage">
                <img :src="getImgUrl()" width="280" height="280" />
            </div>
            <div class="FoodItemTitle">
                {{foodInfo.foodName}}
            </div>
        </div>
    </div>
</template>

<script>
    import { defineComponent } from 'vue';
import FoodPopup from './FoodPopup.vue';
    export default defineComponent({
        data() {
            return {
                isPopupOpen:false,
            }
        },
        components : {
            FoodPopup
        },
        props: {
            foodInfo: {},
        },
        methods:
        {
            getImgUrl() {
                return "src/" + this.foodInfo.imagePath;
            },

            openPopup() {
                this.confirmation = "";
                this.isPopupOpen = true;
            },

            popupConfirmed() {
                alert("Предмет добавлен в корзину!")
                let cart = localStorage.getItem("cart")
                if(cart != null && cart != "") //javascript i hate you; why null autocasts to "null"? WTF?
                    cart = JSON.parse(cart)
                else 
                    cart = []
                cart.push(this.foodInfo)
                localStorage.setItem("cart", JSON.stringify(cart)) 
                

                this.isPopupOpen = false;
            },

        }
    });
</script>


<style scoped>
    .FoodItemCard {
        padding: 15px;
        border: 1px solid gray;
        background: #f5f5f5;
        margin: 5px;
        flex-direction: column;
        gap:0.25rem;
    }

</style>