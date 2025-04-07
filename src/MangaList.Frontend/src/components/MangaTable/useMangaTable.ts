// src/components/useMangaTable.ts
import { ref, watch } from 'vue'
import axios from 'axios'
import type { MangaItem, QueryProps, QueryParams } from './types'

export function useMangaTable() {
  const page = ref<number>(1)
  const items = ref<MangaItem[]>([])
  const loading = ref<boolean>(false)
  const totalItems = ref<number>(0)
  const search = ref('')
  const title = ref('')
  const itemsPerPage = 25

  const headers = [
    { title: 'Cover', key: 'imgUrl', sortable: false },
    { title: 'Title', key: 'title' },
    { title: 'Status', key: 'status', sortable: false },
    { title: 'Volumes', key: 'volumes', sortable: false },
    { title: 'Link', key: 'url', sortable: false },
  ]

  const loadData = async ({ sortBy }: QueryProps) => {
    try {
      loading.value = true
      let params: QueryParams = {
        page: page.value,
        search: title.value,
        key: sortBy?.[0]?.key || 'title',
        order: sortBy?.[0]?.order || 'asc',
      }

      const response = await axios.get('http://localhost:5261/api/manga/all', {
        params,
      })

      items.value = response.data.mangas
      totalItems.value = response.data.mangasTotal
    } catch (error) {
      console.error(error)
    } finally {
      loading.value = false
    }
  }

  watch(title, () => {
    search.value = String(Date.now()) // trigger table filtering
  })

  return {
    page,
    items,
    loading,
    totalItems,
    itemsPerPage,
    search,
    title,
    headers,
    loadData,
  }
}
