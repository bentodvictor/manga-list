<script lang="ts" setup>
import { useMangaTable } from './useMangaTable'

const { page, items, loading, totalItems, itemsPerPage, search, title, headers, loadData } =
  useMangaTable()
</script>

<template>
  <v-container>
    <v-text-field
      v-model="title"
      class="ma-2"
      density="compact"
      placeholder="Search title..."
      hide-details
    />
    <br />
    <v-data-table-server
      :headers="headers"
      :items="items"
      :items-length="totalItems"
      :items-per-page="itemsPerPage"
      :items-per-page-options="[itemsPerPage]"
      :loading="loading"
      :search="search"
      density="compact"
      v-model:page="page"
      @update:options="loadData"
      class="elevation-1"
    >
      <template #item.imgUrl="{ item }">
        <v-avatar size="48" rounded>
          <v-img :src="item.imgUrl" alt="cover" />
        </v-avatar>
      </template>

      <template #item.title="{ item }">
        <strong>{{ item.title }}</strong>
      </template>

      <template #item.url="{ item }">
        <v-btn icon size="x-small" :href="item.url" target="_blank">
          <v-icon size="small">mdi-open-in-new</v-icon>
        </v-btn>
      </template>
    </v-data-table-server>
  </v-container>
</template>
