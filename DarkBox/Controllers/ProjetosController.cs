using DarkBox.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DarkBox.Controllers
{
    [Authorize]
    public class ProjetosController : Controller
    {
        private readonly AppDbContext _context;

        public ProjetosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CriarProjeto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarProjeto(CriarProjetoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Recupera o ID do usuário autenticado
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Criar o projeto na tabela Projects
            var novoProjeto = new Project
            {
                Title = model.Nome,
                Description = model.Descricao,
                ClientId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Projects.Add(novoProjeto);
            await _context.SaveChangesAsync(); // Garante que o ProjectID seja gerado

            // Diretório de upload
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Processar os arquivos enviados
            if (model.Arquivos != null && model.Arquivos.Count > 0)
            {
                bool isFirstFile = true; // Flag para definir o Readme no primeiro arquivo

                foreach (var arquivo in model.Arquivos)
                {
                    string filePath = Path.Combine(uploadPath, arquivo.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await arquivo.CopyToAsync(stream);
                    }

                    // Criar entrada na tabela ProjectFiles
                    var projetoArquivo = new ProjectFile
                    {
                        ProjectId = novoProjeto.ProjectId, // Referência ao projeto criado
                        UploadedBy = userId,
                        FileName = arquivo.FileName,
                        FileSizeMb = (decimal)Math.Round(arquivo.Length / (1024.0 * 1024.0), 2), // Convertendo para MB
                        FilePath = "/uploads/" + arquivo.FileName,
                        UploadedAt = DateTime.UtcNow,
                        Readme = isFirstFile ? model.Readme : null // Apenas o primeiro arquivo recebe o Readme
                    };

                    _context.ProjectFiles.Add(projetoArquivo);
                    isFirstFile = false; // Depois do primeiro arquivo, o restante não recebe Readme
                }

                await _context.SaveChangesAsync();
            }

            // Redireciona para a página de detalhes do projeto
            return RedirectToAction("Detalhes", "Projetos", new { id = novoProjeto.ProjectId });
        }

        [Authorize(Roles = "client")]
        public IActionResult Editar(int id)
        {
            // Recupera o ID do usuário autenticado
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Login");
            }

            // Recupera o projeto do cliente
            var projeto = _context.Projects
                .FirstOrDefault(p => p.ProjectId == id && p.ClientId == userId);

            if (projeto == null)
            {
                return NotFound();  // Caso o projeto não exista ou não pertença ao cliente
            }

            // Retorna os dados do projeto para a view de edição
            var model = new EditarProjetoViewModel
            {
                ProjectId = projeto.ProjectId,
                Title = projeto.Title,
                Description = projeto.Description,
                UploadedAt = projeto.CreatedAt
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Editar(EditarProjetoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);  // Retorna a view caso haja erros no modelo
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var projeto = await _context.Projects
                .FirstOrDefaultAsync(p => p.ProjectId == model.ProjectId && p.ClientId == userId);

            if (projeto == null)
            {
                // Adicione um log para ajudar na depuração
                Console.WriteLine($"Projeto não encontrado. ProjectId: {model.ProjectId}, ClientId: {userId}");
                return NotFound();
            }

            // Atualiza os campos do projeto
            projeto.Title = model.Title;
            projeto.Description = model.Description;

            _context.Projects.Update(projeto);
            await _context.SaveChangesAsync();  // Salva as alterações no banco de dados

            return RedirectToAction("Detalhes", "Projetos", new { id = projeto.ProjectId });
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Excluir(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var projeto = await _context.Projects
                .Include(p => p.ProjectFiles) // Include related ProjectFiles
                .FirstOrDefaultAsync(p => p.ProjectId == id && p.ClientId == userId);

            if (projeto == null)
            {
                return NotFound();
            }

            // Remover os arquivos associados ao projeto
            foreach (var arquivo in projeto.ProjectFiles)
            {
                // Apaga os arquivos fisicamente do servidor
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", arquivo.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            // Apagar o projeto
            _context.Projects.Remove(projeto);
            await _context.SaveChangesAsync();

            return RedirectToAction("ClienteDashboard", "Dashboard");  // Ou para onde você preferir
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var projeto = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectFiles)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (projeto == null)
            {
                return NotFound();
            }

            var viewModel = new ProjetoDetalhesViewModel
            {
                ProjectID = projeto.ProjectId,
                UploadedAt = projeto.CreatedAt ?? DateTime.UtcNow,
                ReadmeHtml = projeto.ProjectFiles.FirstOrDefault()?.Readme,
                Arquivos = projeto.ProjectFiles.ToList(),
                Notificacoes = await _context.Notifications.Where(n => n.UserId == projeto.ClientId).ToListAsync(),
                Projetos = await _context.Projects.Where(p => p.ClientId == projeto.ClientId).ToListAsync()
            };

            return View(viewModel);
        }
    }
}




