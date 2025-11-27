import { apiFetch } from '@/lib/api';
import { API_ENDPOINTS } from '@/config/api';
import type { Cliente, LoginRequest, RegisterRequest, AuthResponse } from '@/types';

export const authService = {
  async login(data: LoginRequest): Promise<AuthResponse> {
    const response = await apiFetch<AuthResponse>(API_ENDPOINTS.CLIENTES_LOGIN, {
      method: 'POST',
      body: JSON.stringify(data),
    });

    if (response.token) {
      localStorage.setItem('auth_token', response.token);
      localStorage.setItem('cliente', JSON.stringify(response.cliente));
    }

    return response;
  },

  async register(data: RegisterRequest): Promise<AuthResponse> {
    const response = await apiFetch<AuthResponse>(API_ENDPOINTS.CLIENTES_REGISTRAR, {
      method: 'POST',
      body: JSON.stringify(data),
    });

    if (response.token) {
      localStorage.setItem('auth_token', response.token);
      localStorage.setItem('cliente', JSON.stringify(response.cliente));
    }

    return response;
  },

  async getMe(): Promise<Cliente> {
    return apiFetch<Cliente>(API_ENDPOINTS.CLIENTES_ME, {
      requiresAuth: true,
    });
  },

  logout() {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('cliente');
  },

  getStoredCliente(): Cliente | null {
    const stored = localStorage.getItem('cliente');
    return stored ? JSON.parse(stored) : null;
  },

  isAuthenticated(): boolean {
    return !!localStorage.getItem('auth_token');
  },
};
