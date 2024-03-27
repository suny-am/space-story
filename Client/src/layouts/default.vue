<template>
  <v-app>
    <AppHeader />
    <v-main>
      <router-view />
    </v-main>

    <AppFooter />
  </v-app>
</template>

<script lang="ts" setup></script>

<style lang="scss">
body {
  animation: fadeIn linear forwards 500ms;
}

@keyframes fadeIn {
  0% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}
</style>
