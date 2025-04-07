<script setup lang="ts">
defineProps<{
  msg: string
}>()
</script>

<template>
  <div class="text-center w-100 pa-5">
    <h1 class="pa-10 text-h1">{{ msg }}</h1>
    <h5 class="pa-4 text-h5">
      MangaList is a fullstack application that automatically syncs and displays paginated manga
      data from the Jikan API, a public API for MyAnimeList.
    </h5>
  </div>
</template>

<style scoped>
h1 {
  color: teal;
  text-shadow: 2px 2px 5px #252525;
}
h5 {
  color: #252525;
  font-style: italic;
}
</style>
