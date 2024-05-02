<template>
    <div class="foodItem" v-for="item in post" :key="item.title">
        <FoodItem :title="item.foodName" :imagePath="item.imagePath" />
    </div>

</template>


<script>
import FoodItem from './FoodItem.vue'
import { defineComponent } from 'vue';

export default defineComponent({
    components: {
        FoodItem
    },

    data() {
        return {
            loading: false,
            post: null,
            items: null
        };
    },
    created() {
        this.fetchData();
    },
    watch: {
        '$route': 'fetchData'
    },
    methods: {
        fetchData() {
            this.post = null;
            this.loading = true;

            fetch('http://localhost:5174/api/Food', {
                method: "GET",
                headers: {
                    accept: "application/json",
                },
            })
                .then(r => r.json())
                .then(json => {
                    this.post = json;
                    this.loading = false;
                    return;
                });
        }
    },
});
</script>


<style>
</style>
