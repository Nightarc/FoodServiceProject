<template>
    <button class="myButton">
        <slot>Нажми меня</slot>
    </button>
</template>

<script>
export default { 
}
</script>

<style scoped>
.myButton {
    border: 1px solid teal;
    background-color: teal;
    color:white;
    align-self: flex-end;
}
</style>