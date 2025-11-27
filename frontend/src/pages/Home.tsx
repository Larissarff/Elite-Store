import { useState, useEffect } from 'react';
import { Navbar } from '@/components/layout/Navbar';
import { ProductCard } from '@/components/products/ProductCard';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import { Search, Filter } from 'lucide-react';
import { productService } from '@/services/productService';
import type { Produto } from '@/types';
import { useToast } from '@/hooks/use-toast';
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

const Home = () => {
  const [produtos, setProdutos] = useState<Produto[]>([]);
  const [loading, setLoading] = useState(true);
  const [busca, setBusca] = useState('');
  const [categoria, setCategoria] = useState<string>('');
  const { toast } = useToast();

  const categorias = ['Eletrônicos', 'Moda', 'Casa', 'Esportes', 'Livros', 'Beleza'];

  const fetchProdutos = async () => {
    setLoading(true);
    try {
      const data = await productService.getAll({
        busca: busca || undefined,
        categoria: categoria || undefined,
      });
      setProdutos(data);
    } catch (error) {
      toast({
        title: 'Erro ao carregar produtos',
        description: error instanceof Error ? error.message : 'Tente novamente',
        variant: 'destructive',
      });
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchProdutos();
  }, []);

  const handleSearch = () => {
    fetchProdutos();
  };

  return (
    <div className="min-h-screen bg-gradient-hero relative overflow-hidden">
      {/* Elementos decorativos de fundo */}
      <div className="absolute inset-0 overflow-hidden pointer-events-none">
        <div className="absolute top-0 left-0 w-96 h-96 bg-primary/5 rounded-full blur-3xl floating" style={{ animationDelay: '0s' }} />
        <div className="absolute top-1/4 right-0 w-96 h-96 bg-accent/5 rounded-full blur-3xl floating" style={{ animationDelay: '2s' }} />
        <div className="absolute bottom-0 left-1/3 w-96 h-96 bg-primary/5 rounded-full blur-3xl floating" style={{ animationDelay: '4s' }} />
      </div>

      <div className="relative z-10">
        <Navbar />
        
        {/* Hero Section */}
        <section className="container mx-auto px-4 py-20 md:py-28 text-center relative">
          <div className="absolute inset-0 bg-gradient-mesh opacity-50" />
          
          <div className="relative z-10">
            <h1 className="text-5xl md:text-7xl lg:text-8xl font-serif font-bold text-gradient-primary mb-6 animate-slide-up leading-tight">
              Bem-vindo à<br />Élite Store
            </h1>
            <p className="text-lg md:text-xl lg:text-2xl text-muted-foreground max-w-3xl mx-auto mb-10 animate-fade-in leading-relaxed" style={{ animationDelay: '200ms' }}>
              Descubra produtos exclusivos com qualidade premium e design sofisticado
            </p>
            
            {/* Decorative elements */}
            <div className="flex justify-center gap-3 mb-8 animate-scale-in" style={{ animationDelay: '400ms' }}>
              <div className="w-16 h-1 bg-gradient-primary rounded-full" />
              <div className="w-8 h-1 bg-accent rounded-full" />
              <div className="w-16 h-1 bg-gradient-primary rounded-full" />
            </div>
          </div>
        </section>
      </div>

      {/* Search and Filters */}
      <section className="container mx-auto px-4 mb-16 relative z-10">
        <div className="max-w-4xl mx-auto">
          <div className="glassmorphism rounded-3xl p-6 shadow-elegant-lg backdrop-blur-xl">
            <div className="flex flex-col md:flex-row gap-4">
              <div className="flex-1 flex gap-3">
                <div className="flex-1 relative group">
                  <Input
                    type="text"
                    placeholder="Buscar produtos..."
                    value={busca}
                    onChange={(e) => setBusca(e.target.value)}
                    onKeyDown={(e) => e.key === 'Enter' && handleSearch()}
                    className="flex-1 bg-background/50 border-border/50 focus:bg-background focus:border-primary/50 transition-all duration-300 rounded-2xl h-12 pl-4"
                  />
                </div>
                <Button 
                  onClick={handleSearch}
                  className="bg-gradient-primary hover:opacity-90 hover:shadow-accent transition-all duration-300 hover:scale-105 rounded-2xl px-6 group"
                >
                  <Search className="h-5 w-5 transition-transform duration-300 group-hover:scale-110" />
                </Button>
              </div>
              
              <Select value={categoria} onValueChange={setCategoria}>
                <SelectTrigger className="w-full md:w-[220px] bg-background/50 border-border/50 hover:bg-background transition-all duration-300 rounded-2xl h-12">
                  <Filter className="h-4 w-4 mr-2" />
                  <SelectValue placeholder="Categoria" />
                </SelectTrigger>
                <SelectContent className="rounded-2xl">
                  <SelectItem value="todas">Todas</SelectItem>
                  {categorias.map((cat) => (
                    <SelectItem key={cat} value={cat}>
                      {cat}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>
          </div>
        </div>
      </section>

      {/* Products Grid */}
      <section className="container mx-auto px-4 pb-20 relative z-10">
        {loading ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8">
            {[...Array(8)].map((_, i) => (
              <div key={i} className="h-[420px] bg-gradient-to-br from-muted to-primary-light/10 animate-pulse rounded-3xl" />
            ))}
          </div>
        ) : produtos.length === 0 ? (
          <div className="text-center py-20">
            <div className="inline-block p-8 glassmorphism rounded-3xl animate-scale-in">
              <p className="text-xl text-muted-foreground font-medium">
                Nenhum produto encontrado
              </p>
            </div>
          </div>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8">
            {produtos.map((produto, index) => (
              <div
                key={produto.idProduto}
                className="animate-slide-up"
                style={{ 
                  animationDelay: `${index * 80}ms`,
                  animationFillMode: 'both'
                }}
              >
                <ProductCard produto={produto} />
              </div>
            ))}
          </div>
        )}
      </section>
    </div>
  );
};

export default Home;
