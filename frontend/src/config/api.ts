export const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL ?? "https://localhost:5001";
  
export const API_ENDPOINTS = {
  // Clientes
  CLIENTES_REGISTRAR: '/clientes/registrar',
  CLIENTES_LOGIN: '/clientes/login',
  CLIENTES_ME: '/clientes/me',
  
  // Produtos
  PRODUTOS: '/produtos',
  PRODUTO_BY_ID: (id: number) => `/produtos/${id}`,
  
  // Carrinho
  CARRINHO: '/carrinho',
  CARRINHO_ITENS: '/carrinho/itens',
  CARRINHO_ITEM_BY_ID: (id: number) => `/carrinho/itens/${id}`,
  
  // Pedidos
  PEDIDOS: '/pedidos',
  PEDIDO_BY_ID: (id: number) => `/pedidos/${id}`,
  
  // Pagamentos
  PAGAMENTOS: '/pagamentos',
} as const;
