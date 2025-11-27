import { apiFetch } from '@/lib/api';
import { API_ENDPOINTS } from '@/config/api';
import type { Pedido, PagamentoRequest } from '@/types';

export const orderService = {
  async createOrder(): Promise<Pedido> {
    return apiFetch<Pedido>(API_ENDPOINTS.PEDIDOS, {
      method: 'POST',
      requiresAuth: true,
    });
  },

  async getOrders(): Promise<Pedido[]> {
    return apiFetch<Pedido[]>(API_ENDPOINTS.PEDIDOS, {
      requiresAuth: true,
    });
  },

  async getOrderById(id: number): Promise<Pedido> {
    return apiFetch<Pedido>(API_ENDPOINTS.PEDIDO_BY_ID(id), {
      requiresAuth: true,
    });
  },

  async confirmPayment(data: PagamentoRequest): Promise<void> {
    return apiFetch<void>(API_ENDPOINTS.PAGAMENTOS, {
      method: 'POST',
      body: JSON.stringify(data),
      requiresAuth: true,
    });
  },
};
