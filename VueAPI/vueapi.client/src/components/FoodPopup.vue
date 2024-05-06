<template>
  <div v-if="isOpen" class="backdrop" @click="close">
    <div class="popup" @click.stop>
      <h1><slot name="title"></slot></h1>
      <hr />
      <div class="FoodPopupImage">
        <img :src="getImgUrl()" width="280" height="280" />
      </div>
      <slot name ="description"></slot> <br>
      Состав: <slot name ="components"></slot> <br>
      Цена: <slot name ="price"></slot> Р <br>
      <hr />
      <div class="footer">
        <slot name="actions" :close="close" :addToCart="addToCart">
          <button @click="close">Отмена</button>
          &nbsp;
          <button @click="addToCart">Добавить в корзину!</button>
        </slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    imagePath: String,
    isOpen: {
      type: Boolean,
      required: true,
    },
  },

  emits: {
    ok: null,
    close: null,
  },

  mounted() {
    document.addEventListener("keydown", this.handleKeydown);
  },
  beforeUnmount() {
    document.removeEventListener("keydown", this.handleKeydown);
  },

  methods: {
    handleKeydown(e) {
      if (this.isOpen && e.key === "Escape") {
        this.close();
      }
    },

    close() {
      this.$emit("close");
    },
    addToCart() {
      this.$emit("ok");
    },
    getImgUrl() {
      return "src/" + this.imagePath;
    },
  },
};
</script>

<style>
.popup {
  top: 50px;
  padding: 20px;
  left: 50%;
  transform: translateX(-50%);
  position: fixed;
  z-index: 101;
  background-color: white;
  border-radius: 10px;
}

.popup h1 {
  text-align: center;
  margin: 0;
}

.backdrop {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.8);
  z-index: 100;
}

.footer {
  text-align: right;
}
</style>
