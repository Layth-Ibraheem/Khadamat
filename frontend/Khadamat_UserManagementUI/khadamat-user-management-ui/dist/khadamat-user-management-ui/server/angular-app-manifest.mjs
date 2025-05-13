
export default {
  bootstrap: () => import('./main.server.mjs').then(m => m.default),
  inlineCriticalCss: true,
  baseHref: '/',
  locale: undefined,
  routes: undefined,
  entryPointToBrowserMapping: {},
  assets: {
    'index.csr.html': {size: 28045, hash: 'b92431f653a3ce7291d2ac899af5fc95bbcc84a00a875075043bedd950dbc7f9', text: () => import('./assets-chunks/index_csr_html.mjs').then(m => m.default)},
    'index.server.html': {size: 17204, hash: '329e053e80b129b715ee690c88cabed5ccdcc5932df9407b75ba100d9c7d2759', text: () => import('./assets-chunks/index_server_html.mjs').then(m => m.default)},
    'styles-2SFIC7N6.css': {size: 237746, hash: 'k4lsGMHAeNY', text: () => import('./assets-chunks/styles-2SFIC7N6_css.mjs').then(m => m.default)}
  },
};
