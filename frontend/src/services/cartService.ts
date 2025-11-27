import { apiFetch } from '@/lib/api';
import { API_ENDPOINTS } from '@/config/api';
import type { Carrinho, ItemCarrinho } from '@/types';

export const cartService = {
  async getCart(): Promise<Carrinho> {
    return apiFetch<Carrinho>(API_ENDPOINTS.CARRINHO, {
      requiresAuth: true,
    });
  },

  async addItem(idProduto: number, quantidade: number = 1): Promise<ItemCarrinho> {
    return apiFetch<ItemCarrinho>(API_ENDPOINTS.CARRINHO_ITENS, {
      method: 'POST',
      body: JSON.stringify({ idProduto, quantidade }),
      requiresAuth: true,
    });
  },

  async updateItem(id: number, quantidade: number): Promise<ItemCarrinho> {
    return apiFetch<ItemCarrinho>(API_ENDPOINTS.CARRINHO_ITEM_BY_ID(id), {
      method: 'PUT',
      body: JSON.stringify({ quantidade }),
      requiresAuth: true,
    });
  },

  async removeItem(id: number): Promise<void> {
    return apiFetch<void>(API_ENDPOINTS.CARRINHO_ITEM_BY_ID(id), {
      method: 'DELETE',
      requiresAuth: true,
    });
  },
};
