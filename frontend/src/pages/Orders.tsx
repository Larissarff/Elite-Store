import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Navbar } from '@/components/layout/Navbar';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Package, Calendar, CreditCard } from 'lucide-react';
import { orderService } from '@/services/orderService';
import { authService } from '@/services/authService';
import type { Pedido } from '@/types';
import { useToast } from '@/hooks/use-toast';
import { format } from 'date-fns';
import { ptBR } from 'date-fns/locale';

const Orders = () => {
  const navigate = useNavigate();
  const { toast } = useToast();
  const [pedidos, setPedidos] = useState<Pedido[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!authService.isAuthenticated()) {
      navigate('/login');
      return;
    }
    fetchPedidos();
  }, []);

  const fetchPedidos = async () => {
    try {
      const data = await orderService.getOrders();
      setPedidos(data);
    } catch (error) {
      toast({
        title: 'Erro ao carregar pedidos',
        description: error instanceof Error ? error.message : 'Tente novamente',
        variant: 'destructive',
      });
    } finally {
      setLoading(false);
    }
  };

  const getStatusColor = (status: string) => {
    const statusMap: Record<string, string> = {
      'Pendente': 'bg-yellow-500/20 text-yellow-700 border-yellow-500/30',
      'Processando': 'bg-blue-500/20 text-blue-700 border-blue-500/30',
      'Enviado': 'bg-purple-500/20 text-purple-700 border-purple-500/30',
      'Entregue': 'bg-green-500/20 text-green-700 border-green-500/30',
      'Cancelado': 'bg-red-500/20 text-red-700 border-red-500/30',
    };
    return statusMap[status] || 'bg-muted text-muted-foreground';
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gradient-hero">
        <Navbar />
        <div className="container mx-auto px-4 py-16">
          <div className="animate-pulse space-y-4">
            <div className="h-8 bg-muted rounded w-1/4" />
            <div className="h-32 bg-muted rounded" />
            <div className="h-32 bg-muted rounded" />
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gradient-hero">
      <Navbar />
      
      <div className="container mx-auto px-4 py-16">
        <h1 className="text-4xl font-serif font-bold text-gradient-primary mb-8">
          Meus Pedidos
        </h1>

        {pedidos.length === 0 ? (
          <Card className="border-border shadow-elegant">
            <CardContent className="py-16 text-center">
              <Package className="h-16 w-16 text-muted-foreground mx-auto mb-4" />
              <h3 className="text-xl font-semibold mb-2">Nenhum pedido ainda</h3>
              <p className="text-muted-foreground">
                Seus pedidos aparecerão aqui após a compra
              </p>
            </CardContent>
          </Card>
        ) : (
          <div className="space-y-6">
            {pedidos.map((pedido) => (
              <Card key={pedido.idPedido} className="border-border shadow-elegant hover:shadow-elegant-lg transition-shadow">
                <CardHeader className="border-b border-border">
                  <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
                    <div>
                      <CardTitle className="text-xl font-serif mb-2">
                        Pedido #{pedido.idPedido}
                      </CardTitle>
                      <div className="flex flex-wrap items-center gap-4 text-sm text-muted-foreground">
                        <div className="flex items-center gap-2">
                          <Calendar className="h-4 w-4" />
                          {format(new Date(pedido.dataCriacao), "dd 'de' MMMM 'de' yyyy", { locale: ptBR })}
                        </div>
                        {pedido.formaPagamento && (
                          <div className="flex items-center gap-2">
                            <CreditCard className="h-4 w-4" />
                            {pedido.formaPagamento}
                          </div>
                        )}
                      </div>
                    </div>
                    <Badge className={`${getStatusColor(pedido.status)} border`}>
                      {pedido.status}
                    </Badge>
                  </div>
                </CardHeader>

                <CardContent className="pt-6">
                  <div className="space-y-3 mb-4">
                    {pedido.itens.map((item, index) => (
                      <div key={index} className="flex justify-between items-center py-2 border-b border-border last:border-0">
                        <div>
                          <p className="font-medium">Produto #{item.idProduto}</p>
                          <p className="text-sm text-muted-foreground">
                            Quantidade: {item.quantidade} × R$ {item.preco.toFixed(2)}
                          </p>
                        </div>
                        <p className="font-semibold">
                          R$ {item.subtotal.toFixed(2)}
                        </p>
                      </div>
                    ))}
                  </div>

                  <div className="flex justify-between items-baseline pt-4 border-t border-border">
                    <span className="text-lg font-semibold">Total</span>
                    <span className="text-2xl font-bold text-primary">
                      R$ {pedido.valorTotal.toFixed(2)}
                    </span>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default Orders;
