import { BrowserRouter, Route, Routes } from "react-router-dom"
import { ListarMaterias } from "./Componets/materias"
import { ListarNotas } from "./Componets/notas"
import EstudianteListar from "./Componets/estudiante"


function App() {
  return(
    <BrowserRouter>
      <Routes>
        <Route path="/"element = {<h1><center>Bienvenido</center></h1>} />
        <Route path="estudiante" element = {<EstudianteListar></EstudianteListar>} />
        <Route path="/api/Usuario/listar/materias" element = {<ListarMaterias></ListarMaterias>} />
        <Route path="/api/Usuario/listar/notas" element = {<ListarNotas></ListarNotas>} />
      </Routes>
    </BrowserRouter>
  )
}


export default App
