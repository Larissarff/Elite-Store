import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Navbar } from '@/components/layout/Navbar';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group';
import { Label } from '@/components/ui/label';
import { CreditCard, Smartphone, FileText, CheckCircle } from 'lucide-react';
import { cartService } from '@/services/cartService';
import { orderService } from '@/services/orderService';
import { authService } from '@/services/authService';
import type { Carrinho } from '@/types';
import { useToast } from '@/hooks/use-toast';

const Checkout = () => {
  const navigate = useNavigate();
  const { toast } = useToast();
  const [carrinho, setCarrinho] = useState<Carrinho | null>(null);
  const [loading, setLoading] = useState(true);
  const [processing, setProcessing] = useState(false);
  const [formaPagamento, setFormaPagamento] = useState<'Cartão de crédito' | 'PIX' | 'Boleto'>('PIX');

  useEffect(() => {
    if (!authService.isAuthenticated()) {
      navigate('/login');
      return;
    }
    fetchCarrinho();
  }, []);

  const fetchCarrinho = async () => {
    try {
      const data = await cartService.getCart();
      if (!data || data.itens.length === 0) {
        navigate('/carrinho');
        return;
      }
      setCarrinho(data);
    } catch (error) {
      toast({
        title: 'Erro ao carregar carrinho',
        variant: 'destructive',
      });
      navigate('/carrinho');
    } finally {
      setLoading(false);
    }
  };

  const handleFinalizarPedido = async () => {
    setProcessing(true);
    try {
      // Criar pedido
      const pedido = await orderService.createOrder();
      
      // Confirmar pagamento
      await orderService.confirmPayment({
        idPedido: pedido.idPedido,
        forma: formaPagamento,
      });

      toast({
        title: 'Pedido realizado com sucesso!',
        description: `Pedido #${pedido.idPedido} confirmado`,
      });

      navigate('/pedidos');
    } catch (error) {
      toast({
        title: 'Erro ao finalizar pedido',
        description: error instanceof Error ? error.message : 'Tente novamente',
        variant: 'destructive',
      });
    } finally {
      setProcessing(false);
    }
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gradient-hero">
        <Navbar />
        <div className="container mx-auto px-4 py-16">
          <div className="animate-pulse space-y-4">
            <div className="h-8 bg-muted rounded w-1/4" />
            <div className="h-64 bg-muted rounded" />
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
          Finalizar Pedido
        </h1>

        <div className="grid lg:grid-cols-3 gap-8">
          <div className="lg:col-span-2">
            <Card className="border-border shadow-elegant">
              <CardHeader>
                <CardTitle className="text-2xl font-serif">
                  Forma de Pagamento
                </CardTitle>
              </CardHeader>
              <CardContent>
                <RadioGroup value={formaPagamento} onValueChange={(v) => setFormaPagamento(v as any)}>
                  <div className="space-y-4">
                    <Card className="border-2 border-border hover:border-primary transition-colors cursor-pointer">
                      <CardContent className="p-4">
                        <div className="flex items-center gap-4">
                          <RadioGroupItem value="PIX" id="pix" />
                          <Label htmlFor="pix" className="flex items-center gap-3 cursor-pointer flex-1">
                            <div className="w-10 h-10 rounded-full bg-accent/20 flex items-center justify-center">
                              <Smartphone className="h-5 w-5 text-accent" />
                            </div>
                            <div>
                              <p className="font-semibold">PIX</p>
                              <p className="text-sm text-muted-foreground">
                                Pagamento instantâneo
                              </p>
                            </div>
                          </Label>
                        </div>
                      </CardContent>
                    </Card>

                    <Card className="border-2 border-border hover:border-primary transition-colors cursor-pointer">
                      <CardContent className="p-4">
                        <div className="flex items-center gap-4">
                          <RadioGroupItem value="Cartão de crédito" id="cartao" />
                          <Label htmlFor="cartao" className="flex items-center gap-3 cursor-pointer flex-1">
                            <div className="w-10 h-10 rounded-full bg-primary/20 flex items-center justify-center">
                              <CreditCard className="h-5 w-5 text-primary" />
                            </div>
                            <div>
                              <p className="font-semibold">Cartão de Crédito</p>
                              <p className="text-sm text-muted-foreground">
                                Parcelamento disponível
                              </p>
                            </div>
                          </Label>
                        </div>
                      </CardContent>
                    </Card>

                    <Card className="border-2 border-border hover:border-primary transition-colors cursor-pointer">
                      <CardContent className="p-4">
                        <div className="flex items-center gap-4">
                          <RadioGroupItem value="Boleto" id="boleto" />
                          <Label htmlFor="boleto" className="flex items-center gap-3 cursor-pointer flex-1">
                            <div className="w-10 h-10 rounded-full bg-muted flex items-center justify-center">
                              <FileText className="h-5 w-5 text-foreground" />
                            </div>
                            <div>
                              <p className="font-semibold">Boleto Bancário</p>
                              <p className="text-sm text-muted-foreground">
                                Vencimento em 3 dias úteis
                              </p>
                            </div>
                          </Label>
                        </div>
                      </CardContent>
                    </Card>
                  </div>
                </RadioGroup>
              </CardContent>
            </Card>
          </div>

          <div className="lg:col-span-1">
            <Card className="border-border shadow-elegant sticky top-24">
              <CardHeader>
                <CardTitle className="text-xl font-serif">
                  Resumo do Pedido
                </CardTitle>
              </CardHeader>
              <CardContent>
                <div className="space-y-4 mb-6">
                  {carrinho?.itens.map((item) => (
                    <div key={item.id} className="flex justify-between text-sm">
                      <span className="text-muted-foreground">
                        {item.produto?.nome} (x{item.quantidade})
                      </span>
                      <span>R$ {item.subtotal.toFixed(2)}</span>
                    </div>
                  ))}
                </div>

                <div className="border-t border-border pt-4 mb-6">
                  <div className="flex justify-between items-baseline mb-2">
                    <span className="text-sm text-muted-foreground">Subtotal</span>
                    <span>R$ {carrinho?.total.toFixed(2)}</span>
                  </div>
                  <div className="flex justify-between items-baseline mb-4">
                    <span className="text-sm text-muted-foreground">Frete</span>
                    <span className="text-accent font-medium">Grátis</span>
                  </div>
                  <div className="flex justify-between items-baseline">
                    <span className="text-lg font-semibold">Total</span>
                    <span className="text-2xl font-bold text-primary">
                      R$ {carrinho?.total.toFixed(2)}
                    </span>
                  </div>
                </div>

                <Button 
                  className="w-full bg-gradient-primary hover:opacity-90 h-12 text-base"
                  onClick={handleFinalizarPedido}
                  disabled={processing}
                >
                  {processing ? (
                    'Processando...'
                  ) : (
                    <>
                      <CheckCircle className="h-5 w-5 mr-2" />
                      Confirmar Pedido
                    </>
                  )}
                </Button>

                <p className="text-xs text-center text-muted-foreground mt-4">
                  Ao confirmar, você concorda com nossos termos e condições
                </p>
              </CardContent>
            </Card>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Checkout;
