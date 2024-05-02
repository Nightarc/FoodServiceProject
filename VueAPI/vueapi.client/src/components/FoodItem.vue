<template>
    <div class="FoodItemCard">
        <div class="FoodItem">
            <div class="FoodImage">
                <img :src="getImgUrl()" width="280" height="280" />
            </div>
            <div class="FoodItemTitle">
                {{title}}
            </div>
        </div>
    </div>
</template>

<script>
    import { defineComponent } from 'vue';

    export default defineComponent({
        props: {
            title: String,
            imagePath: String
        },
        methods:
        {
            getImgUrl() {
                return "src/" + this.imagePath;
            }
        }
    });
</script>


<style scoped>
    .FoodItemCard {
        padding: 15px;
        border: 1px solid gray;
        background: #f5f5f5;
        margin: 5px;
    }

</style>