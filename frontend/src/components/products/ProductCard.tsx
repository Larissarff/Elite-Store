import { ShoppingCart } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardFooter } from '@/components/ui/card';
import type { Produto } from '@/types';
import { useState } from 'react';
import { cartService } from '@/services/cartService';
import { useToast } from '@/hooks/use-toast';
import { authService } from '@/services/authService';
import { useNavigate } from 'react-router-dom';

interface ProductCardProps {
  produto: Produto;
}

export const ProductCard = ({ produto }: ProductCardProps) => {
  const [loading, setLoading] = useState(false);
  const { toast } = useToast();
  const navigate = useNavigate();

  const handleAddToCart = async () => {
    if (!authService.isAuthenticated()) {
      toast({
        title: 'Autenticação necessária',
        description: 'Faça login para adicionar produtos ao carrinho',
        variant: 'destructive',
      });
      navigate('/login');
      return;
    }

    setLoading(true);
    try {
      await cartService.addItem(produto.idProduto);
      toast({
        title: 'Produto adicionado!',
        description: `${produto.nome} foi adicionado ao seu carrinho`,
      });
    } catch (error) {
      toast({
        title: 'Erro ao adicionar',
        description: error instanceof Error ? error.message : 'Tente novamente',
        variant: 'destructive',
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <Card className="group overflow-hidden border-border hover:shadow-elegant-lg transition-all duration-500 hover:-translate-y-2 hover:border-primary/30">
      <div className="aspect-square overflow-hidden bg-gradient-to-br from-muted to-primary-light/20 relative">
        <img
          src={produto.imagemUrl || 'https://images.unsplash.com/photo-1523275335684-37898b6baf30'}
          alt={produto.nome}
          className="h-full w-full object-cover transition-all duration-700 group-hover:scale-110 group-hover:rotate-2"
        />
        <div className="absolute inset-0 bg-gradient-to-t from-background/80 to-transparent opacity-0 group-hover:opacity-100 transition-opacity duration-500" />
      </div>
      
      <CardContent className="p-5 relative">
        <div className="mb-2 transform transition-transform duration-300 group-hover:translate-x-1">
          <span className="inline-block px-3 py-1 text-xs font-semibold text-accent-foreground bg-accent/20 rounded-full uppercase tracking-wide backdrop-blur-sm">
            {produto.categoria}
          </span>
        </div>
        <h3 className="text-lg font-serif font-semibold text-foreground mb-2 line-clamp-2 transition-colors duration-300 group-hover:text-primary">
          {produto.nome}
        </h3>
        <p className="text-sm text-muted-foreground line-clamp-2 mb-4 transition-all duration-300 group-hover:text-foreground">
          {produto.descricao}
        </p>
        <div className="flex items-baseline gap-2">
          <span className="text-2xl font-bold text-gradient-primary transition-all duration-300 group-hover:scale-110 inline-block origin-left">
            R$ {produto.preco.toFixed(2)}
          </span>
          {produto.tamanho && (
            <span className="text-xs text-muted-foreground bg-muted px-2 py-1 rounded-full">
              {produto.tamanho}
            </span>
          )}
        </div>
      </CardContent>

      <CardFooter className="p-5 pt-0">
        <Button 
          onClick={handleAddToCart}
          disabled={loading || produto.estoque === 0}
          className="w-full bg-gradient-primary hover:opacity-90 hover:shadow-accent transition-all duration-300 hover:scale-105 group/btn"
        >
          <ShoppingCart className="h-4 w-4 mr-2 transition-transform duration-300 group-hover/btn:rotate-12" />
          {produto.estoque === 0 ? 'Esgotado' : 'Adicionar ao Carrinho'}
        </Button>
      </CardFooter>
    </Card>
  );
};
