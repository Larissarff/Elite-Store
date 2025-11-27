import { apiFetch } from '@/lib/api';
import { API_ENDPOINTS } from '@/config/api';
import type { Produto } from '@/types';

export const productService = {
  async getAll(filters?: { categoria?: string; busca?: string }): Promise<Produto[]> {
    const params = new URLSearchParams();
    
    if (filters?.categoria) {
      params.append('categoria', filters.categoria);
    }
    
    if (filters?.busca) {
      params.append('busca', filters.busca);
    }

    const query = params.toString();
    const endpoint = query ? `${API_ENDPOINTS.PRODUTOS}?${query}` : API_ENDPOINTS.PRODUTOS;

    return apiFetch<Produto[]>(endpoint);
  },

  async getById(id: number): Promise<Produto> {
    return apiFetch<Produto>(API_ENDPOINTS.PRODUTO_BY_ID(id));
  },
};
