export interface Cliente {
  idCliente: number;
  nome: string;
  email: string;
  endereco?: string;
}

export interface Produto {
  idProduto: number;
  nome: string;
  descricao: string;
  preco: number;
  tamanho?: string;
  estoque: number;
  categoria: string;
  imagemUrl?: string;
}

export interface ItemCarrinho {
  id: number;
  idProduto: number;
  produto?: Produto;
  quantidade: number;
  subtotal: number;
}

export interface Carrinho {
  itens: ItemCarrinho[];
  total: number;
}

export interface ItemPedido {
  idProduto: number;
  quantidade: number;
  preco: number;
  subtotal: number;
}

export interface Pedido {
  idPedido: number;
  idCliente: number;
  itens: ItemPedido[];
  valorTotal: number;
  status: string;
  dataCriacao: string;
  formaPagamento?: string;
}

export interface LoginRequest {
  email: string;
  senha: string;
}

export interface RegisterRequest {
  nome: string;
  email: string;
  senha: string;
  endereco?: string;
}

export interface AuthResponse {
  token?: string;
  cliente: Cliente;
}

export interface PagamentoRequest {
  idPedido: number;
  forma: 'Cartão de crédito' | 'PIX' | 'Boleto';
}
