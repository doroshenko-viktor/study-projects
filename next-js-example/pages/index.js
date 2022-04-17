import Head from 'next/head'
import Script from 'next/script'
import Link from 'next/link'
import Layout from '../components/layout'
import { getSortedPostsData } from '../lib/posts';
import utilStyles from '../styles/utils.module.css';

export default function Home({ posts }) {
  return (
    <Layout home>
      <Head>
        <title>Next.js Example application</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <Script
        src="https://connect.facebook.net/en_US/sdk.js"
        strategy="lazyOnload"
        onLoad={() =>
          console.log(`script loaded correctly, window.FB has been populated`)
        }
      />

      <main>
        <h1 className="title">
          Welcome to <a href="https://nextjs.org">Next.js!</a>
        </h1>

        <Link href="/posts/first-post">first post</Link>
      </main>

      <section className={`${utilStyles.headingMd} ${utilStyles.padding1px}`}>
        <h2 className={utilStyles.headingLg}>Blog</h2>
        <ul className={utilStyles.list}>
          {posts.map(({ id, date, title }) => (
            <li className={utilStyles.listItem} key={id}>
              {title}
              <br />
              {id}
              <br />
              {date}
            </li>
          ))}
        </ul>
      </section>
    </Layout>
  )
}

export async function getStaticProps() {
  const posts = getSortedPostsData();
  return {
    props: {
      posts
    }
  };
}