<template>
  <v-app-bar>
    <v-app-bar-title>
      <h1 class="text-h5 font-weight-bold">
        <a href="/">space story</a>
      </h1>
    </v-app-bar-title>
    <v-btn v-for="item in items">
      <a :href="item.href">
        {{ item.title }}
      </a>
    </v-btn>
  </v-app-bar>
</template>

<script setup lang="ts">
const items = [
  { title: "new game", href: "/game" },
  { title: "load game", href: "/load-game" },
  { title: "options", href: "/options" },
];
</script>

<style lang="scss" scoped>
@import url("https://fonts.googleapis.com/css2?family=Aldrich&family=Anta&family=Audiowide&family=Bruno+Ace&family=Bruno+Ace+SC&family=Electrolize&family=Gemunu+Libre:wght@200..800&family=Goldman:wght@400;700&family=Orbitron:wght@400..900&family=Rationale&family=Sarpanch:wght@400;500;600;700;800;900&family=Wallpoet&family=Zen+Dots&display=swap");

a {
  text-decoration: none;
  color: white !important;
}
.v-app-bar-title {
  h1 {
    font-family: "Bruno Ace SC";
  }
}
</style>
