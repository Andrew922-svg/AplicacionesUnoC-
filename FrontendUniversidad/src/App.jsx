import { BrowserRouter, Route, Routes } from "react-router-dom"
import { ListarMaterias } from "./Componets/materias"
import { ListarNotas } from "./Componets/notas"
import EstudianteListar from "./Componets/estudiante"
import PagHome from "./Componets/home"


function App() {
  return(
    <BrowserRouter>
      <Routes>
        <Route path="/"element = {<PagHome></PagHome>} />
        <Route path="estudiante" element = {<EstudianteListar></EstudianteListar>} />
        <Route path="/api/Usuario/listar/materias" element = {<ListarMaterias></ListarMaterias>} />
        <Route path="/api/Usuario/listar/notas" element = {<ListarNotas></ListarNotas>} />
      </Routes>
    </BrowserRouter>
  )
}


export default App
