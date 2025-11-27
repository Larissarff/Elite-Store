import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Navbar } from '@/components/layout/Navbar';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs';
import { authService } from '@/services/authService';
import { useToast } from '@/hooks/use-toast';
import { LogIn, UserPlus } from 'lucide-react';

const Login = () => {
  const navigate = useNavigate();
  const { toast } = useToast();
  const [loading, setLoading] = useState(false);

  const [loginData, setLoginData] = useState({ email: '', senha: '' });
  const [registerData, setRegisterData] = useState({
    nome: '',
    email: '',
    senha: '',
    endereco: '',
  });

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      await authService.login(loginData);
      toast({
        title: 'Login realizado!',
        description: 'Bem-vindo de volta',
      });
      navigate('/');
    } catch (error) {
      toast({
        title: 'Erro no login',
        description: error instanceof Error ? error.message : 'Verifique suas credenciais',
        variant: 'destructive',
      });
    } finally {
      setLoading(false);
    }
  };

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      await authService.register(registerData);
      toast({
        title: 'Conta criada!',
        description: 'Bem-vindo à Élite Store',
      });
      navigate('/');
    } catch (error) {
      toast({
        title: 'Erro no cadastro',
        description: error instanceof Error ? error.message : 'Tente novamente',
        variant: 'destructive',
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-hero relative overflow-hidden">
      {/* Elementos decorativos de fundo */}
      <div className="absolute inset-0 overflow-hidden pointer-events-none">
        <div className="absolute top-20 left-10 w-72 h-72 bg-primary/10 rounded-full blur-3xl floating" style={{ animationDelay: '0s' }} />
        <div className="absolute bottom-20 right-10 w-80 h-80 bg-accent/10 rounded-full blur-3xl floating" style={{ animationDelay: '3s' }} />
      </div>

      <div className="relative z-10">
        <Navbar />
      
      <div className="container mx-auto px-4 py-16 md:py-20">
        <div className="max-w-md mx-auto">
          <div className="text-center mb-10 animate-slide-up">
            <h1 className="text-4xl md:text-5xl font-serif font-bold text-gradient-primary mb-3">
              Conecte-se
            </h1>
            <p className="text-muted-foreground text-lg">
              Entre ou crie sua conta para continuar
            </p>
            
            {/* Decorative line */}
            <div className="flex justify-center gap-2 mt-6">
              <div className="w-12 h-1 bg-gradient-primary rounded-full" />
              <div className="w-6 h-1 bg-accent rounded-full" />
            </div>
          </div>

          <Tabs defaultValue="login" className="w-full animate-fade-in" style={{ animationDelay: '200ms' }}>
            <TabsList className="grid w-full grid-cols-2 mb-8 p-1 bg-muted/50 rounded-2xl backdrop-blur-sm">
              <TabsTrigger 
                value="login" 
                className="rounded-xl transition-all duration-300 data-[state=active]:shadow-elegant"
              >
                Entrar
              </TabsTrigger>
              <TabsTrigger 
                value="register"
                className="rounded-xl transition-all duration-300 data-[state=active]:shadow-elegant"
              >
                Criar Conta
              </TabsTrigger>
            </TabsList>

            <TabsContent value="login">
              <Card className="border-border shadow-elegant-lg backdrop-blur-sm bg-background/80 rounded-3xl overflow-hidden hover:shadow-accent transition-all duration-500">
                <CardHeader className="space-y-1 pb-6">
                  <CardTitle className="flex items-center gap-2 text-2xl">
                    <div className="w-10 h-10 rounded-2xl bg-gradient-primary flex items-center justify-center">
                      <LogIn className="h-5 w-5 text-primary-foreground" />
                    </div>
                    Login
                  </CardTitle>
                  <CardDescription className="text-base">
                    Entre com suas credenciais
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <form onSubmit={handleLogin} className="space-y-5">
                    <div className="space-y-2">
                      <Label htmlFor="login-email" className="text-sm font-medium">E-mail</Label>
                      <Input
                        id="login-email"
                        type="email"
                        placeholder="seu@email.com"
                        value={loginData.email}
                        onChange={(e) => setLoginData({ ...loginData, email: e.target.value })}
                        required
                        className="h-12 rounded-2xl border-border/50 focus:border-primary transition-all duration-300"
                      />
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="login-senha" className="text-sm font-medium">Senha</Label>
                      <Input
                        id="login-senha"
                        type="password"
                        placeholder="••••••••"
                        value={loginData.senha}
                        onChange={(e) => setLoginData({ ...loginData, senha: e.target.value })}
                        required
                        className="h-12 rounded-2xl border-border/50 focus:border-primary transition-all duration-300"
                      />
                    </div>
                    <Button 
                      type="submit" 
                      className="w-full bg-gradient-primary hover:opacity-90 hover:shadow-accent h-12 text-base rounded-2xl transition-all duration-300 hover:scale-[1.02]"
                      disabled={loading}
                    >
                      {loading ? 'Entrando...' : 'Entrar'}
                    </Button>
                  </form>
                </CardContent>
              </Card>
            </TabsContent>

            <TabsContent value="register">
              <Card className="border-border shadow-elegant-lg backdrop-blur-sm bg-background/80 rounded-3xl overflow-hidden hover:shadow-accent transition-all duration-500">
                <CardHeader className="space-y-1 pb-6">
                  <CardTitle className="flex items-center gap-2 text-2xl">
                    <div className="w-10 h-10 rounded-2xl bg-gradient-accent flex items-center justify-center">
                      <UserPlus className="h-5 w-5 text-accent-foreground" />
                    </div>
                    Criar Conta
                  </CardTitle>
                  <CardDescription className="text-base">
                    Preencha seus dados para cadastro
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <form onSubmit={handleRegister} className="space-y-5">
                    <div className="space-y-2">
                      <Label htmlFor="register-nome" className="text-sm font-medium">Nome Completo</Label>
                      <Input
                        id="register-nome"
                        type="text"
                        placeholder="Seu nome"
                        value={registerData.nome}
                        onChange={(e) => setRegisterData({ ...registerData, nome: e.target.value })}
                        required
                        className="h-12 rounded-2xl border-border/50 focus:border-primary transition-all duration-300"
                      />
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="register-email" className="text-sm font-medium">E-mail</Label>
                      <Input
                        id="register-email"
                        type="email"
                        placeholder="seu@email.com"
                        value={registerData.email}
                        onChange={(e) => setRegisterData({ ...registerData, email: e.target.value })}
                        required
                        className="h-12 rounded-2xl border-border/50 focus:border-primary transition-all duration-300"
                      />
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="register-senha" className="text-sm font-medium">Senha</Label>
                      <Input
                        id="register-senha"
                        type="password"
                        placeholder="••••••••"
                        value={registerData.senha}
                        onChange={(e) => setRegisterData({ ...registerData, senha: e.target.value })}
                        required
                        className="h-12 rounded-2xl border-border/50 focus:border-primary transition-all duration-300"
                      />
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="register-endereco" className="text-sm font-medium">Endereço (Opcional)</Label>
                      <Input
                        id="register-endereco"
                        type="text"
                        placeholder="Rua, número, cidade"
                        value={registerData.endereco}
                        onChange={(e) => setRegisterData({ ...registerData, endereco: e.target.value })}
                        className="h-12 rounded-2xl border-border/50 focus:border-primary transition-all duration-300"
                      />
                    </div>
                    <Button 
                      type="submit" 
                      className="w-full bg-gradient-primary hover:opacity-90 hover:shadow-accent h-12 text-base rounded-2xl transition-all duration-300 hover:scale-[1.02]"
                      disabled={loading}
                    >
                      {loading ? 'Criando...' : 'Criar Conta'}
                    </Button>
                  </form>
                </CardContent>
              </Card>
            </TabsContent>
          </Tabs>
        </div>
      </div>
      </div>
    </div>
  );
};

export default Login;
