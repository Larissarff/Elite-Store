import { Link } from 'react-router-dom';
import { ShoppingCart, User, LogOut, Sparkles } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { authService } from '@/services/authService';
import { useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';
import type { Cliente } from '@/types';

export const Navbar = () => {
  const navigate = useNavigate();
  const [cliente, setCliente] = useState<Cliente | null>(null);

  useEffect(() => {
    setCliente(authService.getStoredCliente());
  }, []);

  const handleLogout = () => {
    authService.logout();
    setCliente(null);
    navigate('/');
  };

  return (
    <nav className="sticky top-0 z-50 w-full border-b border-border/50 bg-background/80 backdrop-blur-xl supports-[backdrop-filter]:bg-background/60 transition-all duration-300">
      <div className="container mx-auto flex h-20 items-center justify-between px-4 animate-fade-in">
        <Link to="/" className="flex items-center gap-2 group">
          <div className="relative">
            <Sparkles className="h-8 w-8 text-accent transition-all duration-500 group-hover:rotate-180 group-hover:scale-110" />
            <div className="absolute inset-0 bg-accent/20 rounded-full blur-xl group-hover:blur-2xl transition-all duration-500 opacity-0 group-hover:opacity-100" />
          </div>
          <span className="text-2xl font-serif font-bold text-gradient-primary transition-all duration-300 group-hover:tracking-wide">
            Élite Store
          </span>
        </Link>

        <div className="hidden md:flex items-center gap-8">
          <Link to="/" className="text-sm font-medium text-foreground hover:text-primary transition-all duration-300 relative group">
            <span className="relative z-10">Início</span>
            <span className="absolute -bottom-1 left-0 w-0 h-0.5 bg-primary transition-all duration-300 group-hover:w-full rounded-full" />
          </Link>
          <Link to="/" className="text-sm font-medium text-foreground hover:text-primary transition-all duration-300 relative group">
            <span className="relative z-10">Produtos</span>
            <span className="absolute -bottom-1 left-0 w-0 h-0.5 bg-primary transition-all duration-300 group-hover:w-full rounded-full" />
          </Link>
        </div>

        <div className="flex items-center gap-3">
          <Button 
            variant="ghost" 
            size="icon" 
            className="relative hover:bg-primary-light transition-all duration-300 hover:scale-110 group"
            onClick={() => navigate('/carrinho')}
          >
            <ShoppingCart className="h-5 w-5 transition-transform duration-300 group-hover:-rotate-12" />
          </Button>

          {cliente ? (
            <div className="flex items-center gap-2">
              <Button 
                variant="ghost" 
                className="hidden md:flex items-center gap-2"
                onClick={() => navigate('/pedidos')}
              >
                <User className="h-4 w-4" />
                <span className="text-sm">{cliente.nome}</span>
              </Button>
              <Button 
                variant="ghost" 
                size="icon"
                onClick={handleLogout}
                className="hover:bg-destructive/10 hover:text-destructive"
              >
                <LogOut className="h-5 w-5" />
              </Button>
            </div>
          ) : (
            <Button 
              onClick={() => navigate('/login')}
              className="bg-gradient-primary text-primary-foreground hover:opacity-90 hover:shadow-accent transition-all duration-300 hover:scale-105"
            >
              Conecte-se
            </Button>
          )}
        </div>
      </div>
    </nav>
  );
};
