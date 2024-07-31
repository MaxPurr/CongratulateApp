import { NavLink } from 'react-router-dom';
import css from '../../css/common/AppBar.module.css'

function AppBar() {
    const getNavLinkStyle = ({isActive}) => {
        const baseStyle = {
            color: "#757575",
            textDecoration: "none",
            display: "inline-block",
            fontWeight: 700,
            fontSize: "20px",
            borderRadius: "5px",
            padding: "8px 10px"
        }
        if(isActive){
            return {
                ...baseStyle,
                color: "#fff",
                backgroundColor: "#3887BE"
            }
        }
        return baseStyle;
    }

    return (
        <nav className={css.menu}>
            <NavLink style={getNavLinkStyle} to="/">
                Главная
            </NavLink>
            <NavLink style={getNavLinkStyle} to="/list">
                Список друзей
            </NavLink>
        </nav>
    );
}

export default AppBar;