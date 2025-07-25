# unihub

DrawIo: https://drive.google.com/file/d/1p-CYF8Kh5j42eQb1ObHBkWp4ufrlQ0-s/view?usp=sharing

UniHub

- 2 Tipos de Usuário
	> Professor
		> Criar Disciplina
		> Convidar alunos para a Disciplina(Email/Link)
		> Criar atividades/trabalhos > Notificar via UniCalendar
		> Publicar notícias/avisos > Notificar via UniCalendar 
		> Upload de materiais
	> Aluno
		> Download de materiais
		> Ver tarefas no UniCalendar
		> Upload de tarefas
		> Ver Status de tarefas (Kanban)

Chat -> Criado automaticamente
	- Todos os usuários tem acesso e podem enviar mensagens

####################  CRIAR CONTA ###################

Professor
	> Criar conta no Clerk 
		> Insert User(Admin) 

	> Criar Disciplina 
		> Insert Disciplina(Admin) 
			> Adicionar Professor(Admin) na Disciplina
			> Gerar código da disciplina (0000)

Aluno
	> Criar conta no Clerk 
		> Insert User(Member) 

	> Inserir código Disciplina
		> Buscar disciplina
			> Adicionar Aluno(Member) na Disciplina

#################### UPLOAD/DOWNLOAD #################

Salva no SupaBase
	> Será enviado a URL 

###################### CHAT ##########################

Usuário
	> Envia mensagem
		> Insert Mensagem
	




