# Instruções de execução

### Pré-requisitos

- .NET SDK 8.0 ou superior
- Stryker.NET 4.16.0

### Executando os testes
Entre no diretório do projeto de testes:
```text
cd ScholarshipEligibility.Tests
```

O comando principal para executar a suíte é:
```text
dotnet test
```
A suíte final possui 23 testes automatizados.
Executando a análise de cobertura

Na pasta ScholarshipEligibility.Tests, execute:
```text
dotnet test --collect:"XPlat Code Coverage"
```


O relatório de cobertura é gerado no diretório:
`TestResults/`
em um arquivo chamado:
`coverage.cobertura.xml`
Executando a análise de mutação

A ferramenta utilizada é o `Stryker.NET versão 4.16.0.`
O Stryker é instalado como ferramenta global:
```text
dotnet tool install --global dotnet-stryker --version 4.16.0
```


Para executar a análise de mutação, estando na pasta ScholarshipEligibility.Tests, utilize:
```text
dotnet-stryker
```


O relatório HTML será gerado automaticamente dentro de:
`StrykerOutput/`

Normalmente, o relatório pode ser encontrado em:
`StrykerOutput/<data-e-hora>/reports/mutation-report.html`

### Comandos principais
**Executar os testes:**
```text
dotnet test
```
**Executar a cobertura:**
```text
dotnet test --collect:"XPlat Code Coverage"
```
**Executar a análise de mutação:**
```text
dotnet-stryker
```





# Relatório
## 1. Identificação

**Aluno:** Rinaldo Junior  
**Linguagem:** C# / .NET 8  
**Framework:** xUnit  
**Ferramenta de mutação:** Stryker.NET 4.16.0  

---

## 2. Objetivo

O objetivo deste trabalho é aplicar técnicas funcionais e estruturais de teste por meio de uma suíte de testes automatizados, avaliar sua adequação e realizar análise de mutação.

O sistema avalia um candidato e retorna `APPROVED`, `REJECTED` ou `MANUAL_REVIEW`, além de uma lista de motivos (`reasons`).

---

## 3. Escolha tecnológica

### C#

Foi escolhida a linguagem C# por familiaridade e pelo ecossistema .NET para automação de testes.

### xUnit

Foi utilizado o xUnit, com testes independentes usando `[Fact]`.

### Stryker.NET

Foi utilizada a versão 4.16.0 do Stryker.NET para geração e avaliação de mutantes.

---

## 4. Projeto dos testes

Os testes foram derivados de duas perspectivas:

- **Funcional:** classes de equivalência e análise de valor-limite.
- **Estrutural:** decisões, branches e diferentes caminhos de execução.

---

## 5. Classes de equivalência

### 5.1 Idade

| Classe | Faixa | Esperado |
|---|---|---|
| Rejeição | `< 16` | `REJECTED` |
| Revisão manual | `16–17` | `MANUAL_REVIEW` |
| Adequada | `>= 18` | sem motivo de idade |

### 5.2 GPA

| Classe | Faixa | Esperado |
|---|---|---|
| Inválida | `< 0` | exceção |
| Rejeição | `0–<6` | `REJECTED` |
| Revisão manual | `6–<7` | `MANUAL_REVIEW` |
| Adequada | `7–10` | sem motivo de GPA |
| Inválida | `> 10` | exceção |

### 5.3 Frequência

| Classe | Faixa | Esperado |
|---|---|---|
| Inválida | `< 0` | exceção |
| Rejeição | `0–<75` | `REJECTED` |
| Revisão manual | `75–<80` | `MANUAL_REVIEW` |
| Adequada | `80–100` | sem motivo de frequência |
| Inválida | `> 100` | exceção |

### 5.4 Cursos obrigatórios

`false` produz rejeição; `true` não adiciona motivo de rejeição.

### 5.5 Registro disciplinar

`true` produz rejeição; `false` não adiciona motivo de rejeição.

---

## 6. Valores-limite considerados

### Idade

`15`, `16`, `17` e `18`.

### GPA

`-0.1`, `0.0`, `5.9`, `6.0`, `6.9`, `7.0`, `10.0` e `10.1`.

### Frequência

`-0.1`, `0.0`, `74.9`, `75.0`, `79.9`, `80.0`, `100.0` e `100.1`.

Os valores `0.0`, `10.0`, `0.0` e `100.0` foram especialmente importantes na análise de mutação.

---

## 7. Decisões e branches testados

Foram exercitadas as principais decisões do sistema:

1. `age < 16`
2. `age <= 17`
3. `gpa < 6.0`
4. `gpa < 7.0`
5. `attendanceRate < 75.0`
6. `attendanceRate < 80.0`
7. `!hasRequiredCourses`
8. `disciplinaryRecord`
9. `rejectionReasons.Count > 0`
10. `reviewReasons.Count > 0`
11. validação de GPA
12. validação de frequência

Também foi testada a prioridade de `REJECTED` sobre `MANUAL_REVIEW`.

---

## 8. Implementação da suíte

A suíte final contém **23 testes automatizados** e atende aos requisitos mínimos: casos de `APPROVED`, `MANUAL_REVIEW`, três motivos diferentes de `REJECTED`, entradas inválidas, valores de fronteira, diferentes decisões e verificação de `Reasons`.

<img width="191" height="140" alt="bc2c585a-4597-4103-91a3-d5f155cb3575" src="https://github.com/user-attachments/assets/8a6cd210-c843-476e-8c35-c038cc3d8e2b" />

---

## 9. Execução dos testes

A execução final apresentou:

```text
Falha:    0
Aprovado: 23
Ignorado: 0
Total:    23
```

---

## 10. Análise de adequação estrutural

A cobertura foi obtida com Coverlet. O relatório registrou **100% de linhas (56/56)** e **100% de branches (28/28)**. O método `EvaluateScholarship` e o método `ValidateInputs` apresentaram cobertura completa das decisões reportadas.

A cobertura indica que os trechos e branches relevantes foram exercitados, mas não garante que os testes diferenciem todas as possíveis alterações de comportamento. Isso foi demonstrado pela análise de mutação.

---

## 11. Análise de mutação – execução inicial

A primeira execução foi realizada com **19 testes**.

| Categoria | Quantidade |
|---|---:|
| Mutantes gerados | 77 |
| Mutantes efetivamente testados | 61 |
| `Killed` | 57 |
| `Survived` | 4 |
| `Ignored` | 12 |
| `CompileError` | 4 |
| `Timeout` | 0 |
| `Errors` | 0 |
| **Score** | **93,44%** |

Cálculo:

```text
57 / (57 + 4) × 100 = 93,44%
```

<img width="2048" height="864" alt="756519ae-fcf3-47a7-8ea2-59bc8b15f342" src="https://github.com/user-attachments/assets/bf2d3fd3-d5f6-4df7-845b-dc4d3d9389ce" />


<img width="847" height="311" alt="006026eb-6aeb-4b12-955e-9fa85e41dad3" src="https://github.com/user-attachments/assets/d4e6f0b0-b7d4-4c2d-b881-327158499c82" />


---

## 12. Análise dos mutantes sobreviventes

Os quatro sobreviventes estavam relacionados aos limites da validação de GPA e frequência.

### Mutante 1 – GPA no limite inferior

Original:

```csharp
if (gpa < 0.0 || gpa > 10.0)
```

Uma mutação possível no limite inferior é:

```csharp
gpa <= 0.0
```

A diferença aparece em `gpa = 0.0`. Como esse valor não era testado na suíte inicial, o mutante sobreviveu.

**Classificação:** lacuna da suíte.

### Mutante 2 – GPA no limite superior

A alteração de `gpa > 10.0` para `gpa >= 10.0` produz comportamento diferente em `gpa = 10.0`.

**Classificação:** lacuna da suíte.

### Mutante 3 – frequência no limite inferior

A alteração de `attendanceRate < 0.0` para `attendanceRate <= 0.0` produz comportamento diferente em `attendanceRate = 0.0`.

**Classificação:** lacuna da suíte.

### Mutante 4 – frequência no limite superior

A alteração de `attendanceRate > 100.0` para `attendanceRate >= 100.0` produz comportamento diferente em `attendanceRate = 100.0`.

**Classificação:** lacuna da suíte.

---

## 13. Testes adicionados após a análise de mutação

Foram adicionados quatro testes específicos para os valores:

```text
GPA = 0.0
GPA = 10.0
Attendance = 0.0
Attendance = 100.0
```

A suíte passou de **19 para 23 testes**. Nenhuma alteração foi feita na lógica do sistema-base.

---

## 14. Análise de mutação – execução final

Após a inclusão dos testes derivados da análise dos sobreviventes:

| Categoria | Inicial | Final |
|---|---:|---:|
| Testes | 19 | 23 |
| Mutantes testados | 61 | 61 |
| `Killed` | 57 | 61 |
| `Survived` | 4 | 0 |
| `Timeout` | 0 | 0 |
| `Errors` | 0 | 0 |
| **Score** | **93,44%** | **100%** |

<img width="722" height="224" alt="image" src="https://github.com/user-attachments/assets/6a6ca595-a41b-4fe4-be4c-83fc46c9dfe2" />

---

## 15. Mutantes ignorados e erros de compilação

Os 12 mutantes `Ignored` eram `Block removal mutation` e foram descartados pelo filtro interno da ferramenta. Eles não foram classificados automaticamente como equivalentes.

Os 4 `CompileError` eram mutações que produziram código que não compilava. Também não foram classificados como equivalentes.

Portanto, os `Ignored` e `CompileError` foram tratados como categorias distintas dos mutantes sobreviventes.

<img width="1886" height="692" alt="Screenshot 2026-09-11 at 18 58 11" src="https://github.com/user-attachments/assets/d83672e3-9b1a-41bf-bdf1-24ae25820552" />
<img width="762" height="94" alt="Screenshot 2026-09-11 at 18 58 09" src="https://github.com/user-attachments/assets/4da14c3b-6d18-4ad4-a7f4-5a088271f52b" />


---

## 16. Mutante equivalente – análise manual

A execução do Stryker não produziu um mutante sobrevivente que pudesse ser honestamente classificado como equivalente. Para atender à análise exigida na atividade, foi construído manualmente um mutante conceitual equivalente.

### Original

```csharp
else if (age <= 17)
{
    reviewReasons.Add(
        "Applicant is under 18 and requires manual review.");
}
```

### Mutante equivalente proposto

```csharp
else if (age < 18)
{
    reviewReasons.Add(
        "Applicant is under 18 and requires manual review.");
}
```

### Justificativa

`age` é do tipo `int`. Para qualquer valor inteiro, `age <= 17` e `age < 18` são verdadeiros exatamente para o mesmo conjunto de valores. Não existe um valor inteiro de `age` capaz de distinguir as duas condições.

Assim, a alteração não modifica o comportamento observável do sistema para nenhum valor possível de `age`.

**Classificação:** mutante equivalente, identificado por análise manual e não pelo Stryker.NET.

---

## 17. O que a mutação revelou que a cobertura não revelou

A cobertura estrutural chegou a 100% antes da análise de mutação, mas o Stryker ainda encontrou quatro sobreviventes. Os quatro apontavam para os limites válidos de GPA e frequência.

Isso demonstra que executar todos os branches não garante que os testes estejam verificando corretamente todos os valores relevantes. A mutação revelou uma lacuna na seleção de dados de teste que a cobertura não evidenciou.

---

## 18. Limitações da suíte

Mesmo com 100% de cobertura e 100% de mutação entre os 61 mutantes efetivamente testados, não é possível afirmar que todos os defeitos possíveis foram eliminados.

Cobertura e mutação dependem do que é representado pelo código e pelos mutantes gerados. Defeitos de especificação, requisitos ausentes, regras de negócio incorretas que sejam reproduzidas pelos próprios testes e classes de defeitos não representadas pelos operadores de mutação podem continuar ocultos.

---

## 19. Por que alta cobertura não garante ausência de defeitos?

Cobertura responde principalmente quais partes do código foram executadas. Ela não garante que o resultado produzido tenha sido verificado com um oráculo suficientemente forte.

Neste trabalho, a suíte tinha 100% de linhas e branches, mas quatro mutantes sobreviveram. As condições alteradas só podiam ser diferenciadas nos valores-limite `0.0`, `10.0` e `100.0`, que ainda não estavam sendo usados adequadamente nos testes iniciais.

A análise de mutação complementou a cobertura justamente por verificar se os testes conseguem detectar alterações no comportamento do código.

---

## 20. Respostas às perguntas

### 1. Quais classes de equivalência foram identificadas?

Foram identificadas classes para idade, GPA, frequência, cursos obrigatórios e registro disciplinar, conforme a Seção 5.

### 2. Quais valores-limite foram considerados?

Foram considerados os limites de idade `15/16/17/18`, GPA `0/6/7/10` e frequência `0/75/80/100`, além de valores imediatamente fora das faixas válidas.

### 3. Quais decisões importantes do código foram testadas?

Idade, GPA, frequência, cursos obrigatórios, registro disciplinar, validação das entradas, prioridade de rejeição e revisão manual e geração de motivos.

### 4. Qual foi o score inicial e final? O que explica a diferença?

O score inicial foi **93,44%**, com 57 mutantes mortos e 4 sobreviventes. Após a análise, foram adicionados quatro testes de fronteira. O score final foi **100%**, com 61 mutantes efetivamente testados e todos mortos.

### 5. A cobertura de código e o score de mutação apontaram para as mesmas lacunas?

Não inicialmente. A cobertura já era 100%, enquanto a mutação revelou quatro lacunas associadas aos valores-limite.

### 6. Quais mutantes foram classificados como equivalentes?

O Stryker não produziu um sobrevivente equivalente. Foi realizada manualmente a análise conceitual da substituição de `age <= 17` por `age < 18`, equivalente porque `age` é inteiro e as duas condições têm exatamente o mesmo conjunto de valores verdadeiros.

### 7. A suíte pode ser considerada adequada? Por quê?

Para o escopo da atividade, sim. Ela cobre os principais requisitos funcionais, decisões estruturais, limites e motivos observáveis e matou todos os 61 mutantes efetivamente executados na execução final. Ainda assim, nenhuma métrica isolada garante ausência de todos os defeitos.

### 8. Que tipos de defeito nenhuma das duas métricas seria capaz de revelar?

Defeitos não representados pelos mutantes, erros de requisitos, regras de negócio incorretas reproduzidas pelos testes, requisitos ausentes e outros problemas fora do comportamento exercitado pela suíte podem não ser detectados.
