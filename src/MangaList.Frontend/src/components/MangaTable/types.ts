// src/components/types.ts
export interface MangaItem {
  imgUrl: string
  title: string
  status: string
  volumes: number
  url: string
}

export interface QueryProps {
  sortBy?: {
    key: string
    order: string
  }[]
}

export interface QueryParams {
  page?: number
  order?: string
  key?: string
  search?: string
}
