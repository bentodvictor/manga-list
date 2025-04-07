<script setup lang="ts">
import MangaTable from './components/MangaTable/MangaTable.vue'
import MangaTitle from './components/MangaTitle.vue'
</script>

<template>
  <header>
    <MangaTitle msg="Manga List" />
  </header>
  <body>
    <MangaTable />
  </body>
</template>

<style scoped>
header {
  line-height: 1.5;
  max-height: 100vh;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }
}
</style>
