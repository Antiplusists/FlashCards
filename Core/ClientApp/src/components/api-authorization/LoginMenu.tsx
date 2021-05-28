import { Component, Fragment } from 'react';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';

import { styled } from '@material-ui/core/styles';
import { Menu, MenuItem, Avatar, Button } from '@material-ui/core';
import { ButtonLink } from '../CustomButtons/ButtonLink';

const CustomButton = styled(
    ({ ...other }) => (
        <Button {...other} />
    )
)({
    color: 'white',
    backgroundColor: 'rgba(255, 255, 255, 0)',
    borderRadius: '15px',
    padding: '10px 15px',
    fontWeight: 'bold',
    fontSize: 'x-large',
    fontFamily: 'Roboto',
    textTransform: 'none',
    '&:hover': {
        color: 'white',
        background: 'rgba(255, 255, 255, 0.1)',
    },
});

const CustomAvatar = styled(Avatar)({
    width: '60px',
    height: '60px',
    marginLeft: '15px'
});

interface LoginMenuState {
    isAuthenticated: boolean,
    userName: string | null,
    anchorEl: HTMLElement | null
}

export class LoginMenu extends Component<{}, LoginMenuState> {
    private _subscription: number | undefined;

    constructor(props: {}) {
        super(props);
        this.state = {
            isAuthenticated: false,
            userName: null,
            anchorEl: null
        };
    }

    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            userName: user && user.userName
        });
    }

    render() {
        const { isAuthenticated, userName } = this.state;
        if (!isAuthenticated) {
            const registerPath = `/register`;
            const loginPath = `/login`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `/profile`;
            return this.authenticatedView(userName, profilePath);
        }
    }

    authenticatedView(userName: string | null, profilePath: string) {
        return (<Fragment>
            <CustomButton
                onClick={(event: React.MouseEvent<HTMLElement>) => this.setState({ anchorEl: event.currentTarget })}
            >
                {userName}
                <CustomAvatar>{userName ? userName[0] : 'A'}</CustomAvatar>
            </CustomButton>
            <Menu
                anchorEl={this.state.anchorEl}
                open={Boolean(this.state.anchorEl)}
                onClose={() => this.setState({ anchorEl: null })}
                style={{ marginTop: '50px', marginLeft: '10px' }}
            >
                <MenuItem component={Link} to={profilePath}
                    onClick={() => this.setState({ anchorEl: null })}>Профиль</MenuItem>
                <MenuItem component={Link} to='/'
                    onClick={() => { this.setState({ anchorEl: null }); authService.logout(); }}>Выйти</MenuItem>
            </Menu>
        </Fragment>);

    }

    anonymousView(registerPath: string, loginPath: string) {
        return (<Fragment>
            <ButtonLink><Link to={registerPath}>Регистрация</Link></ButtonLink>
            <ButtonLink><Link to={loginPath}>Вход</Link></ButtonLink>
        </Fragment>);
    }
}
